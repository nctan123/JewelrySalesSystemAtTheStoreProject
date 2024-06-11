using JSSATSProject.Repository.Entities;
using JSSATSProject.Repository;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.OrderDetail;
using JSSATSProject.Service.Service.IService;
using AutoMapper;
using JSSATSProject.Service.Models.NewFolder;

namespace JSSATSProject.Service.Service.Service
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderDetailService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseModel> CreateOrderDetailAsync(RequestCreateOrderDetail requestOrderDetail)
        {
            var entity = _mapper.Map<OrderDetail>(requestOrderDetail);
            await _unitOfWork.OrderDetailRepository.InsertAsync(entity);
            await _unitOfWork.SaveAsync();

            return new ResponseModel
            {
                Data = entity,
                MessageError = ""
            };
        }

        public async Task<ResponseModel> GetByOrderIdAsync(int id)
        {
            var entities = await _unitOfWork.OrderDetailRepository.GetAsync(
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

        public async Task<ResponseModel> UpdateOrderDetailAsync(int orderdetailId, RequestUpdateOrderDetail requestOrderDetail)
        {
            try
            {
                var orderdetail = await _unitOfWork.OrderDetailRepository.GetByIDAsync(orderdetailId);
                if (orderdetail != null)
                {

                    _mapper.Map(requestOrderDetail, orderdetail);

                    await _unitOfWork.OrderDetailRepository.UpdateAsync(orderdetail);

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
    }
}
