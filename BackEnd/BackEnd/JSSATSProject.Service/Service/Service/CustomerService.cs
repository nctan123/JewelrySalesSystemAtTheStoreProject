using System;
using System.Linq.Expressions;
using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Repository.CustomLib;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.BuyOrderDetailModel;
using JSSATSProject.Service.Models.BuyOrderModel;
using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models.OrderModel;
using JSSATSProject.Service.Models.PaymentModel;
using JSSATSProject.Service.Models.SellOrderDetailsModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.EntityFrameworkCore.Query;

namespace JSSATSProject.Service.Service.Service;

public class CustomerService : ICustomerService
{
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;

    public CustomerService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseModel> CreateCustomerAsync(RequestCreateCustomer requestCustomer)
    {
        var entity = _mapper.Map<Customer>(requestCustomer);

        entity.CreateDate = CustomLibrary.NowInVietnamTime();

        var point = new Point
        {
            AvailablePoint = 0,
            Totalpoint = 0
        };

        await _unitOfWork.PointRepository.InsertAsync(point);
        await _unitOfWork.SaveAsync();

        entity.PointId = point.Id;
        entity.Point = point;


        await _unitOfWork.CustomerRepository.InsertAsync(entity);
        await _unitOfWork.SaveAsync();

        return new ResponseModel
        {
            Data = entity,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> GetAllAsync(int pageIndex, int pageSize)
    {
        var entities = await _unitOfWork.CustomerRepository.GetAsync(
            orderBy: query => query.OrderByDescending(c => c.CreateDate).ThenBy(c => c.Firstname),
            includeProperties: "Point",
            pageIndex: pageIndex,
            pageSize: pageSize
        );

        var responseCustomers = entities.Select(entity => _mapper.Map<ResponseCustomer>(entity)).ToList();

        // Return the mapped response
        var result = new ResponseModel
        {
            Data = responseCustomers,
            MessageError = ""
        };
        result.TotalElements = await CountAsync();
        result.TotalPages = result.CalculateTotalPageCount(pageSize);
        return result;
    }

    public async Task<ResponseModel> SearchAsync(string searchTerm, int pageIndex = 1, int pageSize = 10)
    {
        Expression<Func<Customer, bool>> filter = customer =>
            customer.Firstname.Contains(searchTerm) ||
            customer.Lastname.Contains(searchTerm) ||
            customer.Phone.Contains(searchTerm);

        var customers = await _unitOfWork.CustomerRepository.GetAsync(
            filter,
            query => query.OrderByDescending(c => c.CreateDate).ThenBy(c => c.Firstname),
            "Point",
            pageIndex,
            pageSize
        );

        var response = customers.Select(customer => _mapper.Map<ResponseCustomer>(customer)).ToList();

        var totalCount = await _unitOfWork.CustomerRepository.CountAsync(filter);
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new ResponseModel
        {
            TotalPages = totalPages,
            TotalElements = totalCount,
            Data = response,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> GetByPhoneAsync(string phoneNumber)
    {
        var entities = await _unitOfWork.CustomerRepository.GetAsync(
            c => c.Phone.Equals(phoneNumber),
            includeProperties: "Point");
        var response = entities.Select(entity => new ResponseCustomer
        {
            Id = entity.Id,
            Firstname = entity.Firstname,
            Lastname = entity.Lastname,
            Phone = entity.Phone,
            Email = entity.Email,
            Gender = entity.Gender,
            Address = entity.Address,
            CreateDate = entity.CreateDate,
            Point = entity.Point
        }).ToList();

        // Return the mapped response
        return new ResponseModel
        {
            Data = response,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> GetEntityByPhoneAsync(string phoneNumber)
    {
        var entity = await _unitOfWork.CustomerRepository.GetAsync(
            c => c.Phone.Equals(phoneNumber),
            includeProperties: "Point,SellOrders,Payments,SpecialDiscountRequests");

        // Return the mapped response
        return new ResponseModel
        {
            Data = entity.FirstOrDefault(),
            MessageError = ""
        };
    }

    public async Task<ResponseModel> UpdateCustomerAsync(int customerId, RequestUpdateCustomer requestCustomer)
    {
        try
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIDAsync(customerId);
            if (customer != null)
            {
                var id = customer.Id;
                _mapper.Map(requestCustomer, customer);
                customer.Id = id;
                await _unitOfWork.CustomerRepository.UpdateAsync(customer);

                return new ResponseModel
                {
                    Data = customer,
                    MessageError = ""
                };
            }

            return new ResponseModel
            {
                Data = null,
                MessageError = "Not Found"
            };
        }
        catch (Exception ex)
        {
            
            return new ResponseModel
            {
                Data = null,
                MessageError = "An error occurred while updating the customer: " + ex.Message
            };
        }
    }

    public async Task<ResponseModel> CountNewCustomer(DateTime startDate, DateTime endDate)
    {
        var customers = await _unitOfWork.CustomerRepository.GetAsync(
            c => c.CreateDate >= startDate && c.CreateDate <= endDate
        );

        var count = customers.Count();

        return new ResponseModel
        {
            MessageError = "",
            Data = count
        };
    }

    public async Task<int> CountAsync(Expression<Func<Customer, bool>> filter = null)
    {
        return await _unitOfWork.CustomerRepository.CountAsync(filter);
    }

    public async Task<ResponseModel> GetSellOrdersByPhoneAsync(string phoneNumber, int pageIndex, int pageSize)
    {
        try
        {
            
            var customerEntity = (await _unitOfWork.CustomerRepository.GetAsync(
                    c => c.Phone.Equals(phoneNumber),
                    includeProperties:
                    "SellOrders,SellOrders.SellOrderDetails,SellOrders.Staff," +
                    "SellOrders.SpecialDiscountRequest,SellOrders.Payments.PaymentDetails.PaymentMethod," +
                    "SellOrders.SellOrderDetails.Product," + "SellOrders.SellOrderDetails.Promotion"))
                .FirstOrDefault();

            if (customerEntity == null)
            {
                return new ResponseModel
                {
                    Data = null,
                    MessageError = "Customer not found",
                    TotalPages = 0,
                    TotalElements = 0
                };
            }

            
            var totalCount = customerEntity.SellOrders.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            
            var paginatedOrders = customerEntity.SellOrders
                .OrderByDescending(order => order.CreateDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            
            var sellOrders = new List<ResponseSellOrder>();
            foreach (var order in paginatedOrders)
            {
                var responseSellOrder = _mapper.Map<ResponseSellOrder>(order);

                // Calculate final price
                responseSellOrder.FinalAmount = await GetFinalPriceAsync(order);

                // Map sell order details
                responseSellOrder.SellOrderDetails =
                    _mapper.Map<List<ResponseSellOrderDetails>>(order.SellOrderDetails);

                sellOrders.Add(responseSellOrder);
            }

            return new ResponseModel
            {
                TotalPages = totalPages,
                TotalElements = totalCount,
                Data = sellOrders,
                MessageError = ""
            };
        }
        catch (Exception ex)
        {
        
            return new ResponseModel
            {
                Data = null,
                MessageError = $"An error occurred: {ex.Message}",
                TotalPages = 0,
                TotalElements = 0
            };
        }
    }


    public async Task<ResponseModel> GetPaymentsByPhoneAsync(string phoneNumber, int pageIndex, int pageSize)
    {
        try
        {
            
            var customer = await _unitOfWork.CustomerRepository.GetAsync(
                c => c.Phone.Equals(phoneNumber),
                includeProperties:
                "Payments,Payments.PaymentDetails,Payments.PaymentDetails.PaymentMethod,Payments.Sellorder,Payments.Buyorder");

            var customerEntity = customer.FirstOrDefault();

            if (customerEntity == null)
            {
                return new ResponseModel
                {
                    Data = null,
                    MessageError = "Customer not found",
                    TotalPages = 0,
                    TotalElements = 0
                };
            }

            
            var totalCount = customerEntity.Payments.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

           
            var payments = customerEntity.Payments
                //.Where(p => p.SellorderId != null)
                .OrderByDescending(payment => payment.CreateDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var responsePayments = _mapper.Map<List<ResponsePayment>>(payments);

            return new ResponseModel
            {
                TotalPages = totalPages,
                TotalElements = totalCount,
                Data = responsePayments,
                MessageError = ""
            };
        }
        catch (Exception ex)
        {
            
            return new ResponseModel
            {
                Data = null,
                MessageError = $"Error retrieving payments: {ex.Message}",
                TotalPages = 0,
                TotalElements = 0
            };
        }
    }

    public async Task<ResponseModel> GetBuyOrdersByPhoneAsync(string phoneNumber, int pageIndex, int pageSize)
    {
       
        var customerEntity = (await _unitOfWork.CustomerRepository.GetAsync(
                c => c.Phone.Equals(phoneNumber),
                includeProperties:
                "BuyOrders,BuyOrders.BuyOrderDetails,BuyOrders.Staff," +
                "BuyOrders.BuyOrderDetails.PurchasePriceRatio," +
                "BuyOrders.BuyOrderDetails.Material,BuyOrders.BuyOrderDetails.CategoryType"))
            .FirstOrDefault();

        if (customerEntity == null)
        {
            return new ResponseModel
            {
                Data = null,
                MessageError = "Customer not found",
                TotalPages = 0,
                TotalElements = 0
            };
        }

        
        var totalCount = customerEntity.BuyOrders.Count();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        var buyOrders = customerEntity.BuyOrders
            .OrderByDescending(order => order.CreateDate)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(order =>
            {
                var responseBuyOrder = _mapper.Map<ResponseBuyOrder>(order);
                responseBuyOrder.BuyOrderDetails =
                    _mapper.Map<List<ResponseBuyOrderDetail>>(order.BuyOrderDetails);
                return responseBuyOrder;
            })
            .ToList();

        return new ResponseModel
        {
            TotalPages = totalPages,
            TotalElements = totalCount,
            Data = buyOrders,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> GetCustomerSummaryAsync(int customerId)
    {
        var customer = await _unitOfWork.CustomerRepository.GetEntityByIdAsync(customerId);

        if (customer == null)
        {
            return new ResponseModel
            {
                MessageError = "Customer not found."
            };
        }

        var completedSellOrders = customer.SellOrders.Where(o => o.Status == OrderConstants.CompletedStatus).ToList();
        var otherSellOrders = customer.SellOrders.Where(o =>
            o.Status != OrderConstants.CompletedStatus && o.Status != OrderConstants.CanceledStatus).ToList();

        decimal completedSellOrderSum = 0;
        foreach (var sellOrder in completedSellOrders)
        {
            completedSellOrderSum += await GetFinalPriceAsync(sellOrder);
        }

        decimal otherSellOrderSum = 0;
        foreach (var sellOrder in otherSellOrders)
        {
            otherSellOrderSum += await GetFinalPriceAsync(sellOrder);
        }

        var completedBuyOrders = customer.BuyOrders.Where(o => o.Status == OrderConstants.CompletedStatus).ToList();
        var otherBuyOrders = customer.BuyOrders.Where(o =>
            o.Status != OrderConstants.CompletedStatus && o.Status != OrderConstants.CanceledStatus).ToList();

        var completedPayments = customer.Payments
            .Where(p => p.Status == PaymentConstants.CompletedStatus && p.SellorderId != null).ToList();
        var pendingPayments = customer.Payments.Where(p =>
            p.Status != PaymentConstants.CompletedStatus && p.Status != PaymentConstants.CanceledStatus &&
            p.SellorderId != null).ToList();

        var orderSummary = new ResponseCustomerSummary
        {
            CustomerPhone = customer.Phone,
            // Buy Orders
            BuyOrderCompletedCount = completedBuyOrders.Count,
            BuyOrderCompletedSum = completedBuyOrders.Sum(o => o.TotalAmount),
            BuyOrderOtherCount = otherBuyOrders.Count,
            BuyOrderOtherSum = otherBuyOrders.Sum(o => o.TotalAmount),
            // Sell Orders
            SellOrderCompletedCount = completedSellOrders.Count,
            SellOrderCompletedSum = completedSellOrderSum,
            SellOrderOtherCount = otherSellOrders.Count,
            SellOrderOtherSum = otherSellOrderSum,
            // Payments
            PaymentCompletedCount = completedPayments.Count,
            PaymentCompletedSum = completedPayments.Sum(p => p.Amount),
            PaymentPendingCount = pendingPayments.Count,
            PaymentPendingSum = pendingPayments.Sum(p => p.Amount)
        };

        return new ResponseModel
        {
            Data = orderSummary
        };
    }


    public async Task<decimal> GetFinalPriceAsync(SellOrder sellOrder)
    {
        var pointRate = await _unitOfWork.CampaignPointRepository.GetPointRate(DateTime.Now);
        var discountPoint = sellOrder.DiscountPoint;
        var specialDiscountRequest = sellOrder.SpecialDiscountRequest;
        var specialDiscountRate = (specialDiscountRequest?.DiscountRate).GetValueOrDefault(0);
        if (specialDiscountRequest?.Status == SpecialDiscountRequestConstants.RejectedStatus) specialDiscountRate = 0;
        decimal finalPrice = (sellOrder!.TotalAmount - discountPoint * pointRate) * (1 - specialDiscountRate);
        return finalPrice;
    }

    public async Task<ResponseModel> SearchSellOrdersAsync(string phone, string orderCode, int pageIndex, int pageSize)
    {
        var customer = await _unitOfWork.CustomerRepository.GetAsync(
            c => c.Phone.Equals(phone),
            includeProperties: "SellOrders,SellOrders.SellOrderDetails,SellOrders.Staff," +
                               "SellOrders.SpecialDiscountRequest,SellOrders.SellOrderDetails.Product");

        var customerEntity = customer.FirstOrDefault();

        if (customerEntity == null)
        {
            return new ResponseModel
            {
                Data = null,
                MessageError = "Customer not found",
                TotalPages = 0,
                TotalElements = 0
            };
        }

        var sellOrders = customerEntity.SellOrders.Where(s => s.Code.Contains(orderCode)).ToList();
        var totalCount = sellOrders.Count;
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        // Paginate the sell orders
        var paginatedOrders = sellOrders
            .OrderByDescending(order => order.CreateDate)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        // Process each sell order asynchronously
        var responseSellOrders = new List<ResponseSellOrder>();
        foreach (var order in paginatedOrders)
        {
            var responseSellOrder = _mapper.Map<ResponseSellOrder>(order);

            // Calculate final price
            responseSellOrder.FinalAmount = await GetFinalPriceAsync(order);

            // Map sell order details
            responseSellOrder.SellOrderDetails =
                _mapper.Map<List<ResponseSellOrderDetails>>(order.SellOrderDetails);

            responseSellOrders.Add(responseSellOrder);
        }

        return new ResponseModel
        {
            TotalPages = totalPages,
            TotalElements = totalCount,
            Data = responseSellOrders,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> SearchPaymentsAsync(string phone, string orderCode, int pageIndex, int pageSize)
    {
        var customer = await _unitOfWork.CustomerRepository.GetAsync(
            c => c.Phone.Equals(phone),
            includeProperties:
            "Payments,Payments.PaymentDetails,Payments.PaymentDetails.PaymentMethod,Payments.Sellorder,Payments.Buyorder");

        var customerEntity = customer.FirstOrDefault();

        if (customerEntity == null)
        {
            return new ResponseModel
            {
                Data = null,
                MessageError = "Customer not found",
                TotalPages = 0,
                TotalElements = 0
            };
        }

        var paymentsQuery = customerEntity.Payments
            .Where(p => (p.Sellorder != null && p.Sellorder.Code.Contains(orderCode)) ||
                        (p.Buyorder != null && p.Buyorder.Code.Contains(orderCode)));

        // Get total count before pagination
        var totalCount = paymentsQuery.Count();

        // Paginate the filtered payments
        var paginatedPayments = paymentsQuery
            .OrderByDescending(payment => payment.CreateDate)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(payment => _mapper.Map<ResponsePayment>(payment))
            .ToList();

        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new ResponseModel
        {
            TotalPages = totalPages,
            TotalElements = totalCount,
            Data = paginatedPayments,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> SearchBuyOrdersAsync(string phone, string orderCode, int pageIndex, int pageSize)
    {
        var customer = await _unitOfWork.CustomerRepository.GetAsync(
            c => c.Phone.Equals(phone),
            includeProperties: "BuyOrders,BuyOrders.BuyOrderDetails,BuyOrders.Staff," +
                               "BuyOrders.BuyOrderDetails.PurchasePriceRatio,BuyOrders.BuyOrderDetails.Material,BuyOrders.BuyOrderDetails.CategoryType");

        var customerEntity = customer.FirstOrDefault();

        if (customerEntity == null)
        {
            return new ResponseModel
            {
                Data = null,
                MessageError = "Customer not found",
                TotalPages = 0,
                TotalElements = 0
            };
        }

        var buyOrders = customerEntity.BuyOrders.Where(b => b.Code.Contains(orderCode)).ToList();
        var totalCount = buyOrders.Count;
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        var paginatedBuyOrders = buyOrders
            .OrderByDescending(order => order.CreateDate)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(order =>
            {
                var responseBuyOrder = _mapper.Map<ResponseBuyOrder>(order);
                responseBuyOrder.BuyOrderDetails = _mapper.Map<List<ResponseBuyOrderDetail>>(order.BuyOrderDetails);
                return responseBuyOrder;
            })
            .ToList();

        return new ResponseModel
        {
            TotalPages = totalPages,
            TotalElements = totalCount,
            Data = paginatedBuyOrders,
            MessageError = ""
        };
    }

    public async Task<Dictionary<DateTime, int>> GetCustomersByDateRange(DateTime startDate, DateTime endDate)
    {
        var result = await _unitOfWork.CustomerRepository.GetCustomersByDateRangeAsync(startDate, endDate);
        return result;
    }
}