using System.Linq.Expressions;
using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.BuyOrderDetailModel;
using JSSATSProject.Service.Models.BuyOrderModel;
using JSSATSProject.Service.Service.IService;

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
            "BuyOrderDetails.PurchasePriceRatio,BuyOrderDetails.Material,BuyOrderDetails.CategoryType",
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

    public Task<ResponseModel> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }


    public async Task<ResponseModel> CreateAsync(BuyOrder entity)
    {
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
            requestCreateBuyOrder.ProductCodesAndEstimatePrices.TryGetValue(productCode, out var price);
            var productObj = await _productService.GetEntityByCodeAsync(productCode);
            var diamond = productObj.ProductDiamonds.First().Diamond;
            var diamondGradingCode = diamond.DiamondGradingCode;
            var purchasePriceRatioId =
                (await _unitOfWork.PurchasePriceRatioRepository.GetEntity(productObj.Category.TypeId, "company sold")).Id;

            //in-company buy orders
            var orderDetail = new BuyOrderDetail
            {
                BuyOrderId = buyOrderId,
                ProductName = productObj.Name,
                CategoryTypeId = productObj.Category.TypeId,
                DiamondGradingCode = diamondGradingCode,
                PurchasePriceRatioId = purchasePriceRatioId,
                MaterialId = productObj.ProductMaterials.First().Material.Id,
                MaterialWeight = productObj.ProductMaterials.First().Weight,
                UnitPrice = price
            };
            result.Add(orderDetail);
        }

        return result;
    }

    public async Task<ICollection<BuyOrderDetail>> CreateOrderDetails(
        RequestCreateNonCompanyBuyOrder requestCreateBuyOrder,
        int buyOrderId)
    {
        var result = new List<BuyOrderDetail>();
        var categoryTypeId = requestCreateBuyOrder.CategoryTypeId;
        var materialId = requestCreateBuyOrder.MaterialId;
        var materialWeight = requestCreateBuyOrder.MaterialWeight;
        var diamondGradingCode = requestCreateBuyOrder.DiamondGradingCode;
        int? purchasePriceRatioId = null;
        decimal price = requestCreateBuyOrder.BuyPrice;

        if (categoryTypeId is ProductConstants.RetailGoldCategoryType or ProductConstants.WholesaleGoldCategoryType)
        {
            price = await _productService.CalculateMaterialBuyPrice(materialId, materialWeight);
        }
        else
        {
            purchasePriceRatioId =
                (await _unitOfWork.PurchasePriceRatioRepository.GetEntity(requestCreateBuyOrder.CategoryTypeId, "non-company sold")).Id;
        }

            //in-company buy orders
            var orderDetail = new BuyOrderDetail
            {
                BuyOrderId = buyOrderId,
                ProductName = requestCreateBuyOrder.ProductName,
                CategoryTypeId = requestCreateBuyOrder.CategoryTypeId,
                DiamondGradingCode = diamondGradingCode,
                PurchasePriceRatioId = purchasePriceRatioId,
                MaterialId = materialId,
                MaterialWeight = materialWeight,
                UnitPrice = price
            };
        result.Add(orderDetail);
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
}