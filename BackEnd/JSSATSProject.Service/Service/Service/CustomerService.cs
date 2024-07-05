using System;
using System.Linq.Expressions;
using AutoMapper;
using JSSATSProject.Repository;
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
            Address = entity.Address
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
            // Log the exception and return an appropriate error response
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
        // Fetch the customer by phone number
        var customerEntity = (await _unitOfWork.CustomerRepository.GetAsync(
                c => c.Phone.Equals(phoneNumber),
                includeProperties:
                "SellOrders,SellOrders.SellOrderDetails,SellOrders.Staff," +
                "SellOrders.SpecialDiscountRequest," +
                "SellOrders.SellOrderDetails.Product"))
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

        // Get the total count of sell orders
        var totalCount = customerEntity.SellOrders.Count();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        // Paginate the sell orders
        var sellOrders = customerEntity.SellOrders
            .OrderByDescending(order => order.CreateDate)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(order =>
                {
                    var responseSellOrder = _mapper.Map<ResponseSellOrder>(order);
                    responseSellOrder.SellOrderDetails =
                        _mapper.Map<List<ResponseSellOrderDetails>>(order.SellOrderDetails);
                    return responseSellOrder;
                })
            .ToList();

        return new ResponseModel
        {
            TotalPages = totalPages,
            TotalElements = totalCount,
            Data = sellOrders,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> GetPaymentsByPhoneAsync(string phoneNumber, int pageIndex, int pageSize)
    {
        try
        {
            // Fetch the customer by phone number
            var customer = await _unitOfWork.CustomerRepository.GetAsync(
                c => c.Phone.Equals(phoneNumber),
                includeProperties: "Payments,Payments.PaymentDetails,Payments.PaymentDetails.PaymentMethod,Payments.Order");

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

            // Get the total count of payments
            var totalCount = customerEntity.Payments.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            // Paginate the payments
            var payments = customerEntity.Payments
                .OrderByDescending(payment => payment.CreateDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Map payments to ResponsePayment DTOs
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
            // Handle exceptions gracefully
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
        // Fetch the customer by phone number
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

        // Get the total count of buy orders
        var totalCount = customerEntity.BuyOrders.Count();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        // Paginate the buy orders
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


}