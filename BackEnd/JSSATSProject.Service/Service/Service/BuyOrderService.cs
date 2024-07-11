using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Repository.CustomLib;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.BuyOrderDetailModel;
using JSSATSProject.Service.Models.BuyOrderModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace JSSATSProject.Service.Service.Service;

public class BuyOrderService : IBuyOrderService
{
    private readonly IBuyOrderDetailService _buyOrderDetailService;
    private readonly IMapper _mapper;
    private readonly IProductService _productService;
    private readonly UnitOfWork _unitOfWork;

    public BuyOrderService(UnitOfWork unitOfWork, IMapper mapper, IBuyOrderDetailService buyOrderDetailService,
        IProductService productService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _buyOrderDetailService = buyOrderDetailService;
        _productService = productService;
    }

    public async Task<ResponseModel> GetAllAsync(List<string> statusList, bool ascending, int pageIndex, int pageSize)
    {
        // Validate the input
        if (statusList == null || !statusList.Any())
            return new ResponseModel
            {
                Data = new List<ResponseBuyOrder>(),
                MessageError = "Status list cannot be empty"
            };

        Expression<Func<BuyOrder, bool>> filter = q => statusList.Contains(q.Status);

        // Fetch entities with filtering, ordering, and pagination
        var entities = await _unitOfWork.BuyOrderRepository.GetAsync(
            // Filter based on the status list
            filter,
            includeProperties:
            "BuyOrderDetails,Customer,Staff," +
            "BuyOrderDetails.PurchasePriceRatio,BuyOrderDetails.Material,BuyOrderDetails.CategoryType,Payments.PaymentDetails.PaymentMethod",
            orderBy: ascending
                ? q => q.OrderBy(p => p.CreateDate)
                : q => q.OrderByDescending(p => p.CreateDate),
            pageSize: pageSize,
            pageIndex: pageIndex);

        // Map entities to response models

        var responseBuyOrders = new List<ResponseBuyOrder>();
        foreach (var entity in entities)
        {
            var responseBuyOrder = _mapper.Map<ResponseBuyOrder>(entity);
            responseBuyOrder.BuyOrderDetails = _mapper.Map<List<ResponseBuyOrderDetail>>(entity.BuyOrderDetails);
            responseBuyOrders.Add(responseBuyOrder);
        }

        // Return the response model
        // Return the response model
        var result = new ResponseModel
        {
            Data = responseBuyOrders,
            MessageError = ""
        };
        result.TotalElements = await CountAsync(filter);
        result.TotalPages = result.CalculateTotalPageCount(pageSize);
        return result;
    }

    public async Task<ResponseModel> GetByIdAsync(int id)
    {
        // Fetch the entities with the required related properties
        var entities = await _unitOfWork.BuyOrderRepository.GetAsync(
            filter: q => q.Id == id,
            includeProperties:
            "BuyOrderDetails,Customer,Staff," +
            "BuyOrderDetails.PurchasePriceRatio,BuyOrderDetails.Material,BuyOrderDetails.CategoryType," +
            "Payments.PaymentDetails.PaymentMethod"
        );

        // Get the first matching entity
        var entity = entities.FirstOrDefault();

        // If entity is not found, return an error message
        if (entity == null)
        {
            return new ResponseModel
            {
                Data = null,
                MessageError = "BuyOrder not found"
            };
        }

        // Map the entity to the response model
        var responseBuyOrder = _mapper.Map<ResponseBuyOrder>(entity);
        responseBuyOrder.BuyOrderDetails = _mapper.Map<List<ResponseBuyOrderDetail>>(entity.BuyOrderDetails);

        // Return the response model
        return new ResponseModel
        {
            Data = responseBuyOrder,
            MessageError = ""
        };
    }



    public async Task<ResponseModel> CreateAsync(BuyOrder entity)
    {
        entity.Code = await GenerateUniqueCodeAsync();
        entity.CreateDate = CustomLibrary.NowInVietnamTime();
        await _unitOfWork.BuyOrderRepository.InsertAsync(entity);
        await _unitOfWork.SaveAsync();

        return new ResponseModel
        {
            Data = entity,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> UpdateAsync(int buyOrderId, BuyOrder entity)
    {
        try
        {
            var buyOrder = await _unitOfWork.BuyOrderRepository.GetEntityAsync(buyOrderId);
            if (buyOrder != null)
            {
                buyOrder = entity;
                await _unitOfWork.BuyOrderRepository.UpdateAsync(buyOrder);

                return new ResponseModel
                {
                    Data = buyOrder,
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

    public async Task<ResponseModel> UpdateAsync(int buyOrderId, RequestUpdateBuyOrderStatus entity)
    {
        try
        {
            var buyOrder = await _unitOfWork.BuyOrderRepository.GetEntityAsync(buyOrderId);
            if (buyOrder != null)
            {
                buyOrder.Status = entity.NewStatus;
                //delete buy order details if order is cancelled
                if (entity.NewStatus == OrderConstants.CanceledStatus)
                {
                    var buyOrderDetails = buyOrder.BuyOrderDetails.ToList();
                    foreach (var buyOrderDetail in buyOrderDetails)
                    {
                        await _unitOfWork.BuyOrderDetailRepository.DeleteAsync(buyOrderDetail);
                    }
                }

                await _unitOfWork.BuyOrderRepository.UpdateAsync(buyOrder);

                return new ResponseModel
                {
                    Data = buyOrder,
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

    public decimal GetPrice(string targetProductCode, Dictionary<string, int> productCodesAndQuantity,
        Dictionary<string, int> productCodesAndEstimatePrices)
    {
        foreach (var product in productCodesAndQuantity)
        {
            var productCode = product.Key;
            var quantity = product.Value;
            if (!targetProductCode.Equals(productCode)) continue;
            if (productCodesAndEstimatePrices.TryGetValue(productCode, out var estimatePrice))
                return quantity * estimatePrice;

            throw new ArgumentException($"The product code {targetProductCode} does not have an estimate price.");
        }

        throw new ArgumentException($"The product code {targetProductCode} does not have an estimate price.");
    }

    public decimal GetTotalAmount(Dictionary<string, int> productCodesAndQuantity,
        Dictionary<string, decimal> productCodesAndEstimatePrices)
    {
        decimal totalAmount = 0;

        foreach (var product in productCodesAndQuantity)
        {
            var productCode = product.Key;
            var quantity = product.Value;
            if (productCodesAndEstimatePrices.TryGetValue(productCode, out var estimatePrice))
                totalAmount += quantity * estimatePrice;
            else
                throw new ArgumentException($"The product code {productCode} does not have an estimate price.");
        }

        return totalAmount;
    }

    public async Task<ICollection<BuyOrderDetail>> CreateOrderDetails(RequestCreateBuyOrder requestCreateBuyOrder,
        int buyOrderId)
    {
        var result = new List<BuyOrderDetail>();
        foreach (var product in requestCreateBuyOrder.ProductCodesAndQuantity)
        {
            var productCode = product.Key;
            var quantity = product.Value;
            requestCreateBuyOrder.ProductCodesAndEstimatePrices.TryGetValue(productCode, out var price);
            var productObj = await _productService.GetEntityByCodeAsync(productCode);
            var diamond = productObj.ProductDiamonds.FirstOrDefault()?.Diamond;
            var diamondGradingCode = diamond?.DiamondGradingCode;
            var purchasePriceRatioId =
                (await _unitOfWork.PurchasePriceRatioRepository.GetEntity(productObj.Category.TypeId, "company sold"))
                ?.Id;

            //in-company buy orders
            var orderDetail = new BuyOrderDetail
            {
                Quantity = quantity,
                BuyOrderId = buyOrderId,
                CategoryTypeId = productObj.Category.TypeId,
                DiamondGradingCode = diamondGradingCode,
                PurchasePriceRatioId = purchasePriceRatioId,
                MaterialId = productObj.ProductMaterials.First().Material.Id,
                MaterialWeight = productObj.ProductMaterials.First().Weight,
                UnitPrice = price,
                ProductName = productObj.Name
            };
            result.Add(orderDetail);
        }

        return result;
    }

    public async Task<ICollection<BuyOrderDetail>> CreateOrderDetails(
        RequestCreateNonCompanyBuyOrder requestCreateBuyOrder,
        int buyOrderId)
    {
        var products = requestCreateBuyOrder.Products;
        var result = new List<BuyOrderDetail>();
        foreach (var product in products)
        {
            var categoryTypeId = product.CategoryTypeId;
            var materialId = product.MaterialId;
            var materialWeight = product.MaterialWeight;
            var diamondGradingCode = product.DiamondGradingCode;
            var quantity = product.Quantity!.Value;
            int? purchasePriceRatioId = null;
            decimal price = product.BuyPrice;

            if (categoryTypeId is ProductConstants.RetailGoldCategoryType or ProductConstants.WholesaleGoldCategoryType)
            {
                price = quantity * await _productService.CalculateMaterialBuyPrice(materialId, materialWeight);
            }
            else
            {
                purchasePriceRatioId =
                    (await _unitOfWork.PurchasePriceRatioRepository.GetEntity(product.CategoryTypeId,
                        "non-company sold"))?.Id;
            }

            //in-company buy orders
            var orderDetail = new BuyOrderDetail
            {
                BuyOrderId = buyOrderId,
                ProductName = product.ProductName,
                Quantity = quantity,
                CategoryTypeId = product.CategoryTypeId,
                DiamondGradingCode = diamondGradingCode,
                PurchasePriceRatioId = purchasePriceRatioId,
                MaterialId = materialId,
                MaterialWeight = materialWeight,
                UnitPrice = price
            };
            result.Add(orderDetail);
        }

        return result;
    }

    public async Task<int?> CountAsync(Expression<Func<BuyOrder, bool>> filter)
    {
        return await _unitOfWork.BuyOrderRepository.CountAsync(filter);
    }

    public async Task<ResponseModel> SearchByCriteriaAsync(List<string> statusList, string customerPhone,
        bool ascending, int pageIndex, int pageSize)
    {
        // Validate the input
        if ((statusList == null || !statusList.Any()) && string.IsNullOrEmpty(customerPhone))
            return new ResponseModel
            {
                Data = new List<ResponseBuyOrder>(),
                MessageError = "Status list and customer phone number cannot both be empty"
            };

        Expression<Func<BuyOrder, bool>> filter = b =>
            (statusList == null || statusList.Contains(b.Status)) &&
            (string.IsNullOrEmpty(customerPhone) || b.Customer.Phone.Contains(customerPhone));

        // Fetch entities with filtering, ordering, and pagination
        var entities = await _unitOfWork.BuyOrderRepository.GetAsync(
            filter,
            includeProperties: "BuyOrderDetails,Customer,Staff," +
                               "BuyOrderDetails.PurchasePriceRatio,BuyOrderDetails.Material,BuyOrderDetails.CategoryType",
            orderBy: ascending
                ? q => q.OrderBy(p => p.CreateDate)
                : q => q.OrderByDescending(p => p.CreateDate),
            pageSize: pageSize,
            pageIndex: pageIndex);

        // Map entities to response models
        var responseBuyOrders = new List<ResponseBuyOrder>();
        foreach (var buyOrder in entities)
        {
            var responseBuyOrder = _mapper.Map<ResponseBuyOrder>(buyOrder);
            responseBuyOrder.BuyOrderDetails = _mapper.Map<List<ResponseBuyOrderDetail>>(buyOrder.BuyOrderDetails);
            responseBuyOrders.Add(responseBuyOrder);
        }

        // Return the response model
        var result = new ResponseModel
        {
            Data = responseBuyOrders,
            MessageError = ""
        };
        result.TotalElements = await CountAsync(filter);
        result.TotalPages = result.CalculateTotalPageCount(pageSize);

        return result;
    }

    public async Task<BuyOrder?> GetEntityByCodeAsync(string code)
    {
        return await _unitOfWork.BuyOrderRepository.GetByCodeAsync(code);
    }

    private async Task<string> GenerateUniqueCodeAsync()
    {
        string newCode;
        do
        {
            var prefix = OrderConstants.BuyOrderCodePrefix;
            newCode = prefix + CustomLibrary.RandomString(14 - prefix.Length);
        } while (await _unitOfWork.Context.BuyOrders.AnyAsync(so => so.Code == newCode));

        return newCode;
    }

    public async Task<ResponseModel> SumTotalAmountOrderByDateTimeAsync(DateTime startDate, DateTime endDate)
    {
        Expression<Func<BuyOrder, bool>> filter = order =>
            order.CreateDate >= startDate && order.CreateDate <= endDate &&
            order.Status.Equals(OrderConstants.CompletedStatus);

        var sum = await _unitOfWork.BuyOrderRepository.SumAsync(filter, order => order.TotalAmount);

        return new ResponseModel
        {
            Data = sum,
            MessageError = sum == 0 ? "Not Found" : null
        };
    }

    public async Task<ResponseModel> CountOrderByDateTimeAsync(DateTime startDate, DateTime endDate)
    {
        Expression<Func<BuyOrder, bool>> filter = order =>
            order.CreateDate >= startDate && order.CreateDate <= endDate &&
            order.Status.Equals(OrderConstants.CompletedStatus);

        var count = await _unitOfWork.BuyOrderRepository.CountAsync(filter);

        return new ResponseModel
        {
            Data = count,
            MessageError = count == 0 ? "Not Found" : null
        };
    }
}