using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.OrderModel;
using JSSATSProject.Service.Service.IService;
using System.Linq.Expressions;
using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Repository.Enums;
using JSSATSProject.Service.Models.ProductModel;
using JSSATSProject.Service.Models.SellOrderDetailsModel;


namespace JSSATSProject.Service.Service.Service
{
    public class SellOrderService : ISellOrderService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;
        private readonly ISellOrderDetailService _sellOrderDetailService;
        private readonly IProductService _productService;

        public SellOrderService(UnitOfWork unitOfWork, IMapper mapper, ICustomerService customerService,
            ISellOrderDetailService sellOrderDetailService, IProductService productService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _customerService = customerService;
            _sellOrderDetailService = sellOrderDetailService;
            _productService = productService;
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
            var totalAmount = sellOrder.SellOrderDetails.Sum(s => s.UnitPrice * s.Quantity) -
                              sellOrder.DiscountPoint * pointRate;
            sellOrder.TotalAmount = totalAmount;
            if (!requestSellOrder.IsSpecialDiscountRequested) sellOrder.Status = OrderConstants.DraftStatus;

            await _unitOfWork.SellOrderRepository.InsertAsync(sellOrder);
            await _unitOfWork.SaveAsync();

            return new ResponseModel
            {
                Data = sellOrder,
                MessageError = ""
            };
        }

        public async Task<ResponseModel> GetAllAsync(bool ascending = true, int pageIndex = 1, int pageSize = 10)
        {
            var entities =
                await _unitOfWork.SellOrderRepository.GetAsync(
                    includeProperties: "SellOrderDetails,Staff,Customer,Payments,SellOrderDetails.Product",
                    orderBy: ascending
                        ? q => q.OrderBy(p => p.CreateDate)
                        : q => q.OrderByDescending(p => p.CreateDate),
                    pageSize: pageSize,
                    pageIndex: pageIndex);
            var result = new List<ResponseSellOrder>();
            foreach (var sellOrder in entities)
            {
                var responseSellOrder = _mapper.Map<ResponseSellOrder>(sellOrder);
                responseSellOrder.SellOrderDetails =
                    _mapper.Map<List<ResponseSellOrderDetails>>(sellOrder.SellOrderDetails);
                result.Add(responseSellOrder);
            }
            return new ResponseModel
            {
                Data = result,
                MessageError = ""
            };
        }

        public async Task<ResponseModel> GetByIdAsync(int id)
        {
            var entities = await _unitOfWork.SellOrderRepository.GetAsync(
                so => so.Id == id,
                includeProperties: "SellOrderDetails,Staff,Customer,Payments");
            var response = _mapper.Map<List<ResponseSellOrder>>(entities);

            return new ResponseModel
            {
                Data = response,
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


        public async Task<ResponseModel> UpdateStatusAsync(int orderId, UpdateSellOrderStatus requestSellOrder)
        {
            try
            {
                var order = await GetEntityByIdAsync(orderId);
                if (order != null)
                {
                    _mapper.Map(requestSellOrder, order);

                    await _unitOfWork.SellOrderRepository.UpdateAsync(order);
                    //neu update status = cancelled
                    if (order.Status.Equals(OrderConstants.CanceledStatus))
                    {
                        await _sellOrderDetailService.UpdateAllOrderDetailsStatus(order, OrderConstants.CanceledStatus);
                    }
                    else if (order.Status.Equals(OrderConstants.CompletedStatus))
                        await _sellOrderDetailService.UpdateAllOrderDetailsStatus(order,
                            SellOrderDetailsConstants.Delivered);

                    //new update status 

                    return new ResponseModel
                    {
                        Data = order,
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

        public async Task<List<ResponseProductForCheckOrder>> GetProducts(SellOrder? sellOrder)
        {
            var products = new List<ResponseProductForCheckOrder>();
            if (sellOrder?.SellOrderDetails is not null)
            {
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


                    var responseProduct = new ResponseProductForCheckOrder()
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
            }

            //add exception handling here
            return products;
        }

        public async Task<ResponseModel> SumTotalAmountOrderByDateTimeAsync(DateTime startDate, DateTime endDate)
        {
            Expression<Func<SellOrder, bool>> filter = order =>
                (order.CreateDate >= startDate) && (order.CreateDate <= endDate) &&
                (order.Status.Equals(OrderConstants.CompletedStatus));

            decimal sum = await _unitOfWork.SellOrderRepository.SumAsync(filter, order => order.TotalAmount);

            return new ResponseModel
            {
                Data = sum,
                MessageError = sum == 0 ? "Not Found" : null,
            };
        }

        public async Task<ResponseModel> CountOrderByDateTimeAsync(DateTime startDate, DateTime endDate)
        {
            Expression<Func<SellOrder, bool>> filter = order =>
                (order.CreateDate >= startDate) && (order.CreateDate <= endDate) &&
                (order.Status.Equals(OrderConstants.CompletedStatus));

            int count = await _unitOfWork.SellOrderRepository.CountAsync(filter);

            return new ResponseModel
            {
                Data = count,
                MessageError = count == 0 ? "Not Found" : null,
            };
        }
    }
}