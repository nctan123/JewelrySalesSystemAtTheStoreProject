using System.Linq.Expressions;
using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Repository.CustomLib;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Repository.Repos;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.OrderModel;
using JSSATSProject.Service.Models.PointModel;
using JSSATSProject.Service.Models.ProductModel;
using JSSATSProject.Service.Models.SellOrderDetailsModel;
using JSSATSProject.Service.Models.SellOrderModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace JSSATSProject.Service.Service.Service;

public class SellOrderService : ISellOrderService
{
    private readonly ICustomerService _customerService;
    private readonly IMapper _mapper;
    private readonly IProductService _productService;
    private readonly ISellOrderDetailService _sellOrderDetailService;
    private readonly IPointService _pointService;
    private readonly UnitOfWork _unitOfWork;

    public SellOrderService(UnitOfWork unitOfWork, IMapper mapper, ICustomerService customerService,
        ISellOrderDetailService sellOrderDetailService, IProductService productService, IPointService pointService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _customerService = customerService;
        _sellOrderDetailService = sellOrderDetailService;
        _productService = productService;
        _pointService = pointService;
    }

    public async Task<int> CountAsync(Expression<Func<SellOrder, bool>> filter = null)
    {
        var response = await _unitOfWork.SellOrderRepository.CountAsync(filter);
        return response;
    }

    public async Task<SellOrder?> GetEntityByCodeAsync(string code)
    {
        var result = await _unitOfWork.SellOrderRepository.GetByCodeAsync(code);
        return result;
    }

    public async Task<ResponseModel> CreateOrderAsync(RequestCreateSellOrder requestSellOrder)
    {
        var customer =
            (Customer)(await _customerService.GetEntityByPhoneAsync(requestSellOrder.CustomerPhoneNumber)).Data!;
        var sellOrder = _mapper.Map<SellOrder>(requestSellOrder);
        var pointRate = await _unitOfWork.CampaignPointRepository.GetPointRate(DateTime.Now);
        sellOrder.Customer = customer;
        //fetch order details
        sellOrder.SellOrderDetails = await _sellOrderDetailService.GetAllEntitiesFromSellOrderAsync(sellOrder.Id,
            requestSellOrder.ProductCodesAndQuantity, requestSellOrder.ProductCodesAndPromotionIds);
        sellOrder.DiscountPoint = requestSellOrder.DiscountPoint;
        var totalAmount = sellOrder.SellOrderDetails.Sum(s => (1 - (s?.Promotion?.DiscountRate).GetValueOrDefault(0)) * s!.UnitPrice);
        sellOrder.TotalAmount = totalAmount;
        sellOrder.Description = requestSellOrder.Description;
        if (!requestSellOrder.IsSpecialDiscountRequested) sellOrder.Status = OrderConstants.DraftStatus;

        // CreateCode
        sellOrder.Code = await GenerateUniqueCodeAsync();

        // SetDateTime
        DateTime vnTime = CustomLibrary.NowInVietnamTime();
        sellOrder.CreateDate = vnTime;

        await _unitOfWork.SellOrderRepository.InsertAsync(sellOrder);
        await _unitOfWork.SaveAsync();
        

        return new ResponseModel
        {
            Data = sellOrder,
            MessageError = ""
        };
    }
    
    public async Task<decimal> GetFinalPriceAsync(SellOrder sellOrder)
    {
        var pointRate = await _unitOfWork.CampaignPointRepository.GetPointRate(DateTime.Now);
        var discountPoint = sellOrder.DiscountPoint;
        var specialDiscountRequest = sellOrder.SpecialDiscountRequest;
        var specialDiscountRate = (specialDiscountRequest?.DiscountRate).GetValueOrDefault(0);
        if (specialDiscountRequest?.Status == SpecialDiscountRequestConstants.RejectedStatus) specialDiscountRate = 0;
        decimal finalPrice = (sellOrder!.TotalAmount - discountPoint * pointRate) * (1-specialDiscountRate);
        return finalPrice;
    }

    public async Task<ResponseModel> GetAllAsync(List<string> statusList, bool ascending = true, int pageIndex = 1,
        int pageSize = 10)
    {
        // Validate the input
        if (statusList == null || !statusList.Any())
            return new ResponseModel
            {
                Data = new List<ResponseSellOrder>(),
                MessageError = "Status list cannot be empty"
            };

        Expression<Func<SellOrder, bool>> filter = s => statusList.Contains(s.Status);

        // Fetch entities with filtering, ordering, and pagination
        var entities = await _unitOfWork.SellOrderRepository.GetAsync(
            // Filter based on the status list
            filter,
            includeProperties:
            "SellOrderDetails,Staff,Customer,Payments,SellOrderDetails.Product," +
            "SpecialDiscountRequest,Payments.PaymentDetails.PaymentMethod,SellOrderDetails.Promotion",
            orderBy: ascending
                ? q => q.OrderBy(p => p.CreateDate)
                : q => q.OrderByDescending(p => p.CreateDate),
            pageSize: pageSize,
            pageIndex: pageIndex);

        // Map entities to response models
        var responseSellOrders = new List<ResponseSellOrder>();
        foreach (var sellOrder in entities)
        {
            var responseSellOrder = _mapper.Map<ResponseSellOrder>(sellOrder);
            responseSellOrder.SellOrderDetails =
                _mapper.Map<List<ResponseSellOrderDetails>>(sellOrder.SellOrderDetails);
            responseSellOrder.FinalAmount = await GetFinalPriceAsync(sellOrder);
            responseSellOrders.Add(responseSellOrder);
        }

        // Return the response model
        var result = new ResponseModel
        {
            Data = responseSellOrders,
            MessageError = ""
        };
        result.TotalElements = await CountAsync(filter);
        result.TotalPages = result.CalculateTotalPageCount(pageSize);
        return result;
    }


    public async Task<ResponseModel> GetByIdAsync(int id)
    {
        var entities = await _unitOfWork.SellOrderRepository.GetAsync(
            so => so.Id == id,
            includeProperties: "SellOrderDetails.Promotion,Staff,Customer,Payments," +
                               "SellOrderDetails.Product,SpecialDiscountRequest," +
                               "Payments.PaymentDetails.PaymentMethod");
        // var response = _mapper.Map<List<ResponseSellOrder>>(entities);

        // Map entities to response models
        var responseSellOrders = new List<ResponseSellOrder>();
        foreach (var sellOrder in entities)
        {
            var responseSellOrder = _mapper.Map<ResponseSellOrder>(sellOrder);
            responseSellOrder.SellOrderDetails =
                _mapper.Map<List<ResponseSellOrderDetails>>(sellOrder.SellOrderDetails);
            responseSellOrder.FinalAmount = await GetFinalPriceAsync(sellOrder);
            responseSellOrders.Add(responseSellOrder);
        }

        return new ResponseModel
        {
            Data = responseSellOrders,
            MessageError = ""
        };
    }

    public async Task<SellOrder> GetEntityByIdAsync(int id)
    {
        var entity = await _unitOfWork.SellOrderRepository.GetEntityByIdAsync(id);
        return entity;
    }

    public async Task<ResponseModel> UpdateOrderAsync(int orderId, RequestUpdateSellOrder requestSellOrder)
    {
        try
        {
            var order = await _unitOfWork.SellOrderRepository.GetByIDAsync(orderId);
            if (order != null)
            {
                _mapper.Map(requestSellOrder, order);

                await _unitOfWork.SellOrderRepository.UpdateAsync(order);

                return new ResponseModel
                {
                    Data = order,
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


    public async Task<ResponseModel> UpdateStatusAsync(int orderId, UpdateSellOrderStatus requestSellOrder)
    {
        try
        {
            var order = await GetEntityByIdAsync(orderId);
            if (order != null)
            {
                _mapper.Map(requestSellOrder, order);
                DateTime vnTime = CustomLibrary.NowInVietnamTime();
                order.CreateDate = vnTime;
                await _unitOfWork.SellOrderRepository.UpdateAsync(order);
                //neu update status = cancelled
                if (order.Status.Equals(OrderConstants.CanceledStatus)) 
                {
                    await _sellOrderDetailService.UpdateAllOrderDetailsStatus(order, OrderConstants.CanceledStatus);

                    //update point 
                    var pointId =  order.Customer.Point.Id;

                    var updatepoint = new RequestUpdatePoint
                    {
                        AvailablePoint = order.DiscountPoint
                    };
                    await _pointService.UpdatePointAsync(pointId, updatepoint);

                }
                else if (order.Status.Equals(OrderConstants.CompletedStatus))
                    await _sellOrderDetailService.UpdateAllOrderDetailsStatus(order,
                        SellOrderDetailsConstants.Delivered);

                //new update status 

                return new ResponseModel
                {
                    Data = order,
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

    public async Task<List<ResponseProductForCheckOrder>> GetProducts(SellOrder? sellOrder)
    {
        var products = new List<ResponseProductForCheckOrder>();
        if (sellOrder?.SellOrderDetails is not null)
            foreach (var orderDetail in sellOrder.SellOrderDetails)
            {
                var product = orderDetail.Product;
                var buybackRate = await _unitOfWork.PurchasePriceRatioRepository.GetRate(product.CategoryId);
                decimal buybackPrice = 0;
                string reason;
                //calculate by current gold price
                if (product.CategoryId.Equals(ProductConstants.RetailGoldCategory)
                    || product.CategoryId.Equals(ProductConstants.WholesaleGoldCategory))
                {
                    buybackPrice = await _productService.CalculateProductPrice(product, orderDetail.Quantity);
                    reason = $"Current market price of gold is {buybackPrice}";
                }
                //calculate by old sell order price
                else
                {
                    buybackPrice = orderDetail.UnitPrice * buybackRate;
                    reason = $"The percentage of buyback value is {buybackRate}";
                }


                var responseProduct = new ResponseProductForCheckOrder
                {
                    Code = product.Code,
                    Name = product.Name,
                    Quantity = orderDetail.Quantity,
                    PriceInOrder = orderDetail.UnitPrice,
                    EstimateBuyPrice = buybackPrice,
                    ReasonForEstimateBuyPrice = reason
                };
                products.Add(responseProduct);
            }

        //add exception handling here
        return products;
    }

    public async Task<ResponseModel> SumTotalAmountOrderByDateTimeAsync(DateTime startDate, DateTime endDate)
    {
        Expression<Func<SellOrder, bool>> filter = order =>
            order.CreateDate >= startDate && order.CreateDate <= endDate &&
            order.Status.Equals(OrderConstants.CompletedStatus);

        var sum = await _unitOfWork.SellOrderRepository.SumAsync(filter, order => order.TotalAmount);

        return new ResponseModel
        {
            Data = sum,
            MessageError = sum == 0 ? "Not Found" : null
        };
    }

    public async Task<ResponseModel> CountOrderByDateTimeAsync(DateTime startDate, DateTime endDate)
    {
        Expression<Func<SellOrder, bool>> filter = order =>
            order.CreateDate >= startDate && order.CreateDate <= endDate &&
            order.Status.Equals(OrderConstants.CompletedStatus);

        var count = await _unitOfWork.SellOrderRepository.CountAsync(filter);

        return new ResponseModel
        {
            Data = count,
            MessageError = count == 0 ? "Not Found" : null
        };
    }

    public async Task<ResponseModel> SearchByAsync(List<string> statusList, string customerPhone,
        bool ascending = true, int pageIndex = 1, int pageSize = 10)
    {
        // Validate the input
        if ((statusList == null || !statusList.Any()) && string.IsNullOrEmpty(customerPhone))
            return new ResponseModel
            {
                Data = new List<ResponseSellOrder>(),
                MessageError = "Status list and customer phone number cannot both be empty"
            };

        // Fetch entities with filtering, ordering, and pagination
        var entities = await _unitOfWork.SellOrderRepository.GetAsync(
            q => (statusList == null || statusList.Contains(q.Status)) &&
                 (string.IsNullOrEmpty(customerPhone) || q.Customer.Phone.Contains(customerPhone)),
            includeProperties:
            "SellOrderDetails,Staff,Customer,Payments,SellOrderDetails.Product,SpecialDiscountRequest",
            orderBy: ascending
                ? q => q.OrderBy(p => p.CreateDate)
                : q => q.OrderByDescending(p => p.CreateDate),
            pageSize: pageSize,
            pageIndex: pageIndex);

        // Map entities to response models
        var result = new List<ResponseSellOrder>();
        foreach (var sellOrder in entities)
        {
            var responseSellOrder = _mapper.Map<ResponseSellOrder>(sellOrder);
            responseSellOrder.SellOrderDetails =
                _mapper.Map<List<ResponseSellOrderDetails>>(sellOrder.SellOrderDetails);
            responseSellOrder.FinalAmount = await GetFinalPriceAsync(sellOrder);
            result.Add(responseSellOrder);
        }

        // Return the response model
        return new ResponseModel
        {
            Data = result,
            MessageError = ""
        };
    }

    private async Task<string> GenerateUniqueCodeAsync()
    {
        string newCode;
        do
        {
            var prefix = OrderConstants.SellOrderCodePrefix;
            newCode = prefix + CustomLibrary.RandomString(14 - prefix.Length);
        }
        while (await _unitOfWork.Context.SellOrders.AnyAsync(so => so.Code == newCode));
        return newCode;
    }

    public async Task<SellOrder> MapOrderAsync(RequestCreateSellOrder requestSellOrder)
    {
        var customer =
            (Customer)(await _customerService.GetEntityByPhoneAsync(requestSellOrder.CustomerPhoneNumber)).Data!;
        var sellOrder = _mapper.Map<SellOrder>(requestSellOrder);
        var pointRate = await _unitOfWork.CampaignPointRepository.GetPointRate(DateTime.Now);
        sellOrder.Customer = customer;
        //fetch order details
        sellOrder.SellOrderDetails = await _sellOrderDetailService.GetAllEntitiesFromSellOrderAsync(sellOrder.Id,
            requestSellOrder.ProductCodesAndQuantity, requestSellOrder.ProductCodesAndPromotionIds);
        sellOrder.DiscountPoint = requestSellOrder.DiscountPoint;
        var totalAmount = sellOrder.SellOrderDetails.Sum(s => (1 - (s?.Promotion?.DiscountRate).GetValueOrDefault(0)) * s!.UnitPrice);
        sellOrder.TotalAmount = totalAmount;
        sellOrder.Description = requestSellOrder.Description;
        return sellOrder;
    }

    public async Task RemoveAllSellOrderDetails(int id)
    {
        var sellOrder = await GetEntityByIdAsync(id);
        var sellOrderDetails = sellOrder.SellOrderDetails.ToList();
        foreach (var sellOrderDetail in sellOrderDetails)
        {
            await _productService.UpdateProductStatusAsync(sellOrderDetail.ProductId, ProductConstants.ActiveStatus);
            await _unitOfWork.SellOrderDetailRepository.DeleteAsync(sellOrderDetail);
        }
    }
    public async Task<ResponseModel> UpdateOrderAsync(int orderId, SellOrder targetOrder)
    {
        try
        {
            var order = await _unitOfWork.SellOrderRepository.GetByIDAsync(orderId);
            if (targetOrder != null)
            {
                order.Customer = targetOrder.Customer;
                order.SellOrderDetails = targetOrder.SellOrderDetails;
                order.DiscountPoint = targetOrder.DiscountPoint;
                order.TotalAmount = targetOrder.TotalAmount;
                order.Description = targetOrder.Description;

                await _unitOfWork.SellOrderRepository.UpdateAsync(order);

                return new ResponseModel
                {
                    Data = order,
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
}