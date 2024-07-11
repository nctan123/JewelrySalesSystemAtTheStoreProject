using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.OrderDetail;
using JSSATSProject.Service.Models.ProductModel;
using JSSATSProject.Service.Models.SellOrderDetailsModel;
using JSSATSProject.Service.Service.IService;
using System.Linq.Expressions;

namespace JSSATSProject.Service.Service.Service;

public class SellOrderDetailService : ISellOrderDetailService
{
    private readonly IMapper _mapper;
    private readonly IProductService _productService;
    private readonly UnitOfWork _unitOfWork;

    public SellOrderDetailService(UnitOfWork unitOfWork, IMapper mapper, IProductService productService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _productService = productService;
    }


    public async Task<ResponseModel> CreateOrderDetailAsync(RequestCreateOrderDetail requestOrderDetail)
    {
        var entity = _mapper.Map<SellOrderDetail>(requestOrderDetail);
        await _unitOfWork.SellOrderDetailRepository.InsertAsync(entity);
        await _unitOfWork.SaveAsync();

        return new ResponseModel
        {
            Data = entity,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> GetByOrderIdAsync(int id)
    {
        var entities = await _unitOfWork.SellOrderDetailRepository.GetAsync(
            c => c.OrderId.Equals(id),
            includeProperties: "Product"
        );

        var response = _mapper.Map<List<ResponseSellOrderDetails>>(entities);

        return new ResponseModel
        {
            Data = response
        };
    }

    public async Task<ResponseModel> UpdateOrderDetailAsync(int orderdetailId,
        RequestUpdateOrderDetail requestOrderDetail)
    {
        try
        {
            var orderdetail = await _unitOfWork.SellOrderDetailRepository.GetEntityByIdAsync(orderdetailId);
            if (orderdetail != null)
            {
                _mapper.Map(requestOrderDetail, orderdetail);

                await _unitOfWork.SellOrderDetailRepository.UpdateAsync(orderdetail);

                return new ResponseModel
                {
                    Data = orderdetail,
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

    public async Task<ResponseModel> CountProductsSoldByCategoryAsync(DateTime startDate, DateTime endDate)
    {
        var orderDetails = await _unitOfWork.SellOrderDetailRepository.GetAsync(
            od => od.Order.CreateDate >= startDate
                  && od.Order.CreateDate <= endDate
                  && od.Status.Equals(SellOrderDetailsConstants.Delivered),
            includeProperties: "Product,Product.Category");


        var productsSoldPerCategory = orderDetails
            .GroupBy(od => od.Product.Category.Name)
            .Select(group => new
            {
                Category = group.Key,
                Quantity = group.Sum(od => od.Quantity)
            })
            .ToList();


        var result = productsSoldPerCategory.Select(item => new Dictionary<string, object>
        {
            { "Category", item.Category },
            { "Quantity", item.Quantity }
        }).ToList();

        return new ResponseModel
        {
            Data = result
        };
    }

    public async Task<List<SellOrderDetail>> GetAllEntitiesFromSellOrderAsync(int sellOrderId,
        Dictionary<string, int> productCodesAndQuantity, Dictionary<string, int?>? productCodesAndPromotionIds)
    {
        var result = new List<SellOrderDetail>();
        foreach (var item in productCodesAndQuantity)
        {
            var productId = item.Key;
            var quantity = item.Value;
            var product = await _productService.GetEntityByCodeAsync(productId);
            product.Status = ProductConstants.InactiveStatus;
            int? promotionId = null;

            productCodesAndPromotionIds?.TryGetValue(item.Key, out promotionId);
        
            var basePrice = await _productService.CalculateProductPrice(product, item.Value);
            Promotion? promotion = null;
            if (promotionId is not null) promotion = await _unitOfWork.PromotionRepository.GetByIDAsync(promotionId.Value);

            var sellOrderDetails = new SellOrderDetail
            {
                ProductId = product.Id,
                Quantity = quantity,
                UnitPrice = basePrice,
                OrderId = sellOrderId,
                PromotionId = promotionId is not null ? Convert.ToInt32(promotionId) : null,
                Promotion = promotion
            };

            result.Add(sellOrderDetails);
        }

        return result;
    }

    public async Task UpdateAllOrderDetailsStatus(SellOrder order, string newStatus)
    {
        var sellOrderDetails = order.SellOrderDetails;
        foreach (var item in sellOrderDetails)
        {
            item.Status = newStatus;
            var requestUpdateOrderDetailsStatus = _mapper.Map<RequestUpdateSellOrderDetailsStatus>(item);
            await UpdateStatusAsync(item.Id, newStatus, requestUpdateOrderDetailsStatus);
        }
    }

    public async Task<ResponseModel> GetTotalRevenueStallAsync(DateTime startDate, DateTime endDate, int pageIndex,
        int pageSize, bool ascending)
    {
        try
        {
            var orderDetails = await _unitOfWork.SellOrderDetailRepository.GetAsync(
                od => od.Order.CreateDate >= startDate
                      && od.Order.CreateDate <= endDate
                      && od.Order.Status.Equals(OrderConstants.CompletedStatus)
                      && od.Status.Equals(SellOrderDetailsConstants.Delivered),
                orderBy =>
                {
                    if (ascending)
                        return orderBy.OrderBy(od => od.Product.Stalls.Name);
                    return orderBy.OrderByDescending(od => od.Product.Stalls.Name);
                },
                "Product,Product.Stalls",
                pageIndex,
                pageSize
            );

            var revenuePerStall = orderDetails
                .GroupBy(od => od.Product.Stalls.Name)
                .Select(group => new
                {
                    StallName = group.Key,
                    TotalRevenue = group.Sum(od => od.Quantity * od.UnitPrice)
                })
                .ToList();

            var result = revenuePerStall.Select(item => new Dictionary<string, object>
            {
                { "StallName", item.StallName },
                { "TotalRevenue", item.TotalRevenue }
            }).ToList();

            return new ResponseModel
            {
                Data = result,
                MessageError = ""
            };
        }
        catch (Exception ex)
        {
            return new ResponseModel
            {
                Data = null,
                MessageError = ex.Message
            };
        }
    }

    public async Task<List<ResponseProductDetails>> GetProductFromSellOrderDetailAsync(int orderId)
    {
        var sellOrderDetails = await _unitOfWork.SellOrderDetailRepository.GetAsync(
            sod => sod.OrderId == orderId,
            includeProperties: "Product"
        );

        var responseProducts = sellOrderDetails
            .Select(sod => new ResponseProductDetails
            {
                Id = sod.Product.Id,
                SellOrderDetailId = sod.Id,
                CategoryId = sod.Product.CategoryId
            })
            .ToList();

        return responseProducts;
    }

    public async Task<ResponseModel> GetProductSoldAsync(bool ascending, int pageIndex, int pageSize)
    {
        // Define the filter for SellOrderDetail
        Expression<Func<SellOrderDetail, bool>> filter = sod => sod.Status.Equals(SellOrderDetailsConstants.Delivered);

        // Get sell order details with the specified filter and ordering
        var sellOrderDetails = await _unitOfWork.SellOrderDetailRepository.GetAsync(
            filter,
            includeProperties: "Product, Guarantees, Promotion, Order",
            orderBy: q => ascending
                ? q.OrderBy(e => e.Product.Name).ThenBy(e => e.UnitPrice)
                : q.OrderByDescending(e => e.Product.Name).ThenByDescending(e => e.UnitPrice),
            pageIndex: pageIndex,
            pageSize: pageSize
        );

        // Prepare the response products list
        var responseProducts = new List<ResponseProductSold>();
        foreach (var sellOrderDetail in sellOrderDetails)
        {
            var guaranteeCode = sellOrderDetail.Guarantees != null && sellOrderDetail.Guarantees.Any()
                ? string.Join(", ", sellOrderDetail.Guarantees.Select(g => g.Code))
                : "Unknown";

            var responseProduct = new ResponseProductSold
            {
                SellOrderCode = sellOrderDetail.Order.Code,
                ProductName = sellOrderDetail.Product.Name,
                UnitPrice = sellOrderDetail.UnitPrice,
                Quantity = sellOrderDetail.Quantity,
                PromotionRate = sellOrderDetail.Promotion?.DiscountRate ?? 0,
                GuaranteeCode = guaranteeCode
            };

            responseProducts.Add(responseProduct);
        }

        // Get the total count using the same filter
        var totalCount = await _unitOfWork.SellOrderDetailRepository.CountAsync(filter);
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        // Prepare the response model
        var responseModel = new ResponseModel
        {
            Data = responseProducts,
            TotalElements = totalCount,
            TotalPages = totalPages,
        };

        return responseModel;
    }

    public async Task<ResponseModel> UpdateStatusAsync(int orderdetailId, string newStatus,
        RequestUpdateSellOrderDetailsStatus newOrderDetails)
    {
        try
        {
            var orderdetail = await _unitOfWork.SellOrderDetailRepository.GetEntityByIdAsync(orderdetailId);
            if (orderdetail != null)
            {
                _mapper.Map(newOrderDetails, orderdetail);
                newOrderDetails.Status = newStatus;
                if (newStatus == SellOrderDetailsConstants.CanceledStatus)
                {
                    orderdetail.Product.Status = ProductConstants.ActiveStatus;
                    await _unitOfWork.ProductRepository.UpdateAsync(orderdetail.Product);
                }

                await _unitOfWork.SellOrderDetailRepository.UpdateAsync(orderdetail);

                return new ResponseModel
                {
                    Data = orderdetail,
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