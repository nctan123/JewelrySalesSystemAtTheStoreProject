using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.OrderModel;
using JSSATSProject.Service.Service.IService;
using System.Linq.Expressions;


namespace JSSATSProject.Service.Service.Service
{
    public class OrderService : IOrderService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel> CreateOrderAsync(RequestCreateOrder requestOrder)
        {
            var entity = _mapper.Map<Order>(requestOrder);
            await _unitOfWork.OrderRepository.InsertAsync(entity);
            await _unitOfWork.SaveAsync();

            return new ResponseModel
            {
                Data = entity,
                MessageError = ""
            };
        }

        public async Task<ResponseModel> GetAllAsync()
        {
            var entities = await _unitOfWork.OrderRepository.GetAsync();
            var response = _mapper.Map<List<ResponseOrder>>(entities);

            return new ResponseModel
            {
                Data = response,
                MessageError = ""
            };
        }

        public async Task<ResponseModel> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.OrderRepository.GetByIDAsync(id);
            var response = _mapper.Map<ResponseOrder>(entity);

            return new ResponseModel
            {
                Data = response,
                MessageError = ""
            };
        }

        public async Task<ResponseModel> UpdateOrderAsync(int orderId, RequestUpdateOrder requestOrder)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetByIDAsync(orderId);
                if (order != null)
                {

                    _mapper.Map(requestOrder, order);

                    await _unitOfWork.OrderRepository.UpdateAsync(order);

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
            Expression<Func<Order, bool>> filter = order =>
                (order.CreateDate >= startDate) && (order.CreateDate <= endDate);

            decimal sum = await _unitOfWork.OrderRepository.SumAsync(filter, order => order.TotalAmount);

            return new ResponseModel
            {
                Data = sum,
                MessageError = sum == 0 ? "Not Found" : null,
            };
        }

        public async Task<ResponseModel> CountOrderByDateTimeAsync(DateTime startDate, DateTime endDate)
        {
            Expression<Func<Order, bool>> filter = order =>
                (order.CreateDate >= startDate) && (order.CreateDate <= endDate);

            int count = await _unitOfWork.OrderRepository.CountAsync(filter);

            return new ResponseModel
            {
                Data = count,
                MessageError = count == 0 ? "Not Found" : null,
            };
        }

        public async Task<ResponseModel> CountOrderByOrderTypeAsync(int month)
        {
            var orders = await _unitOfWork.OrderRepository.GetAsync(
                filter: o => o.CreateDate.Month == month,
                includeProperties: "");

            var ordersByType = orders
                .GroupBy(o => o.Type)
                .Select(group => new
                {
                    Type = group.Key,
                    Quantity = group.Count()
                })
                .ToList();

            var result = ordersByType.Select(item => new Dictionary<string, object>
        {
            { "Type", item.Type },
            { "Quantity", item.Quantity }
        }).ToList();

            return new ResponseModel
            {
                Data = result
            };
        }
    }
}