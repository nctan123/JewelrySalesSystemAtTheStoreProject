using JSSATSProject.Repository.Entities;
using JSSATSProject.Repository;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.OrderDetail;
using JSSATSProject.Service.Service.IService;
using AutoMapper;
using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Service.Models.NewFolder;
using JSSATSProject.Service.Models.SellOrderDetailsModel;

namespace JSSATSProject.Service.Service.Service
{
    public class SellOrderDetailService : ISellOrderDetailService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

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
                c => c.OrderId.Equals(id)
            );
            var response = _mapper.Map<List<ResponseOrderDetail>>(entities);


            foreach (var orderDetail in response)
            {
                if (orderDetail.Product != null)
                {
                    orderDetail.ProductName = orderDetail.Product.Name;
                }
            }

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
                var orderdetail = await _unitOfWork.SellOrderDetailRepository.GetByIDAsync(orderdetailId);
                if (orderdetail != null)
                {
                    _mapper.Map(requestOrderDetail, orderdetail);

                    await _unitOfWork.SellOrderDetailRepository.UpdateAsync(orderdetail);

                    return new ResponseModel
                    {
                        Data = orderdetail,
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

        public async Task<ResponseModel> CountProductsSoldByCategoryAsync(DateTime startDate, DateTime endDate)
        {
            var orderDetails = await _unitOfWork.SellOrderDetailRepository.GetAsync(
                filter: od => od.Order.CreateDate >= startDate
                              && od.Order.CreateDate <= endDate,
                includeProperties: "Product");


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
            Dictionary<string, int> productCodesAndQuantity, Dictionary<string, string> productCodesAndPromotionIds)
        {
            var result = new List<SellOrderDetail>();
            foreach (var item in productCodesAndQuantity)
            {
                var product = await _productService.GetEntityByCodeAsync(item.Key);
                product.Status = "inactive";
                productCodesAndPromotionIds.TryGetValue(item.Key, out string? promotionId);
                var sellOrderDetails = new SellOrderDetail()
                {
                    ProductId = product.Id,
                    Quantity = item.Value,
                    PromotionId = promotionId is not null ? Convert.ToInt32(promotionId) : null,
                    UnitPrice = await _productService.CalculateProductPrice(product, item.Value),
                    OrderId = sellOrderId,
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
    }
}