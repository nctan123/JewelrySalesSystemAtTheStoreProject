using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.OrderModel;
using JSSATSProject.Service.Service.IService;
using System.Linq.Expressions;
using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Repository.Enums;


namespace JSSATSProject.Service.Service.Service
{
    public class SellOrderService : ISellOrderService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;
        private readonly ISellOrderDetailService _sellOrderDetailService;

        public SellOrderService(UnitOfWork unitOfWork, IMapper mapper, ICustomerService customerService,
            ISellOrderDetailService sellOrderDetailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _customerService = customerService;
            _sellOrderDetailService = sellOrderDetailService;
        }

        public async Task<ResponseModel> CreateOrderAsync(RequestCreateSellOrder requestSellOrder)
        {
            var customer =
                (Customer)(await _customerService.GetEntityByPhoneAsync(requestSellOrder.CustomerPhoneNumber)).Data!;
            var sellOrder = _mapper.Map<SellOrder>(requestSellOrder);
            sellOrder.Customer = customer;
            //fetch order details
            sellOrder.SellOrderDetails = await _sellOrderDetailService.GetAllEntitiesFromSellOrderAsync(sellOrder.Id,
                requestSellOrder.ProductCodesAndQuantity, requestSellOrder.ProductCodesAndPromotionIds);
            sellOrder.DiscountPoint = requestSellOrder.DiscountPoint; 
            
            var totalAmount = sellOrder.SellOrderDetails.Sum(s => s.UnitPrice) - sellOrder.DiscountPoint;
            sellOrder.TotalAmount = totalAmount;
            if (!requestSellOrder.IsSpecialDiscountRequested) sellOrder.Status = OrderConstants.ProcessingStatus;
            
            await _unitOfWork.SellOrderRepository.InsertAsync(sellOrder);
            await _unitOfWork.SaveAsync();

            return new ResponseModel
            {
                Data = sellOrder,
                MessageError = ""
            };
        }

        public async Task<ResponseModel> GetAllAsync()
        {
            var entities = await _unitOfWork.SellOrderRepository.GetAsync();
            var response = _mapper.Map<List<ResponseSellOrder>>(entities);

            return new ResponseModel
            {
                Data = response,
                MessageError = ""
            };
        }

        public async Task<ResponseModel> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.SellOrderRepository.GetEntityByIdAsync(id);
            var response = _mapper.Map<ResponseSellOrder>(entity);

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

        public async Task<ResponseModel> SumTotalAmountOrderByDateTimeAsync(DateTime startDate, DateTime endDate)
        {
            // Expression<Func<Order, bool>> filter = order =>
            //     (order.CreateDate >= startDate) && (order.CreateDate <= endDate);
            //
            // decimal sum = await _unitOfWork.SellOrderRepository.SumAsync(filter, order => order.TotalAmount);
            //
            // return new ResponseModel
            // {
            //     Data = sum,
            //     MessageError = sum == 0 ? "Not Found" : null,
            // };
            throw new NotImplementedException();
        }

        public async Task<ResponseModel> CountOrderByDateTimeAsync(DateTime startDate, DateTime endDate)
        {
            // Expression<Func<Order, bool>> filter = order =>
            //     (order.CreateDate >= startDate) && (order.CreateDate <= endDate);
            //
            // int count = await _unitOfWork.SellOrderRepository.CountAsync(filter);
            //
            // return new ResponseModel
            // {
            //     Data = count,
            //     MessageError = count == 0 ? "Not Found" : null,
            // };
            throw new NotImplementedException();
        }

        public async Task<ResponseModel> CountOrderByOrderTypeAsync(int month)
        {
            //     var orders = await _unitOfWork.SellOrderRepository.GetAsync(
            //         filter: o => o.CreateDate.Month == month,
            //         includeProperties: "");
            //
            //     var ordersByType = orders
            //         .GroupBy(o => o.Type)
            //         .Select(group => new
            //         {
            //             Type = group.Key,
            //             Quantity = group.Count()
            //         })
            //         .ToList();
            //
            //     var result = ordersByType.Select(item => new Dictionary<string, object>
            // {
            //     { "Type", item.Type },
            //     { "Quantity", item.Quantity }
            // }).ToList();
            //
            //     return new ResponseModel
            //     {
            //         Data = result
            //     };
            throw new NotImplementedException();
        }
    }
}