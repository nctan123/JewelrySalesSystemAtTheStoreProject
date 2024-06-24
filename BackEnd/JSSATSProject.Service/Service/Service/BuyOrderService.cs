using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.BuyOrderModel;
using JSSATSProject.Service.Service.IService;

namespace JSSATSProject.Service.Service.Service;

public class BuyOrderService : IBuyOrderService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IBuyOrderDetailService _buyOrderDetailService;
    private readonly IProductService _productService;

    public BuyOrderService(UnitOfWork unitOfWork, IMapper mapper, IBuyOrderDetailService buyOrderDetailService, IProductService productService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _buyOrderDetailService = buyOrderDetailService;
        _productService = productService;
    }

    public Task<ResponseModel> GetAllAsync()
    {
        throw new NotImplementedException();
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
            MessageError = "",
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
                    MessageError = "",
                };
            }

            return new ResponseModel
            {
                Data = null,
                MessageError = "Not Found",
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

    public async Task<BuyOrder?> GetEntityByCodeAsync(string code)
    {
        return await _unitOfWork.BuyOrderRepository.GetByCodeAsync(code);
    }

    public decimal GetPrice(string targetProductCode, Dictionary<string, int> productCodesAndQuantity,
        Dictionary<string, int> productCodesAndEstimatePrices)
    {
        foreach (var product in productCodesAndQuantity)
        {
            string productCode = product.Key;
            int quantity = product.Value;
            if (!targetProductCode.Equals(productCode)) continue;
            if (productCodesAndEstimatePrices.TryGetValue(productCode, out int estimatePrice))
            {
                return quantity * estimatePrice;
            }
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
            string productCode = product.Key;
            int quantity = product.Value;
            if (productCodesAndEstimatePrices.TryGetValue(productCode, out decimal estimatePrice))
            {
                totalAmount += quantity * estimatePrice;
            }
            else
            {
                throw new ArgumentException($"The product code {productCode} does not have an estimate price.");
            }
        }

        return totalAmount;
    }

    public async Task<ICollection<BuyOrderDetail>> CreateOrderDetails(RequestCreateBuyOrder requestCreateBuyOrder, int buyOrderId)
    {
        var result = new List<BuyOrderDetail>();
        foreach (var product in requestCreateBuyOrder.ProductCodesAndQuantity)
        {
            var productCode = product.Key;
            var quantity = product.Value;
            requestCreateBuyOrder.ProductCodesAndEstimatePrices.TryGetValue(productCode, out decimal price);
            var productObj = await _productService.GetEntityByCodeAsync(productCode);
            var diamond = productObj.ProductDiamonds.First().Diamond;
            var diamondGradingCode = diamond.DiamondGradingCode;
            var purchasePriceRatioId = (await _unitOfWork.PurchasePriceRatioRepository.GetEntity(productObj.Category.TypeId)).Id;
            
            //in-company buy orders
            var orderDetail = new BuyOrderDetail()
            {
                BuyOrderId = buyOrderId,
                CategoryTypeId = productObj.Category.TypeId,
                DiamondGradingCode = diamondGradingCode,
                PurchasePriceRatioId = purchasePriceRatioId,
                MaterialId = productObj.ProductMaterials.First().Material.Id,
                MaterialWeight = productObj.ProductMaterials.First().Weight,
                UnitPrice = price,
            };
            result.Add(orderDetail);
        }

        return result;
    }
}