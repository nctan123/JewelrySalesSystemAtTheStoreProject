using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.OrderModel;
using JSSATSProject.Service.Service.IService;


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
                    order = _mapper.Map<Order>(requestOrder);
                    await   _unitOfWork.OrderRepository.UpdateAsync(order);
                    await _unitOfWork.SaveAsync();

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
                    MessageError = "An error occurred while updating the order: " + ex.Message
                };
            }
        }
    }
}