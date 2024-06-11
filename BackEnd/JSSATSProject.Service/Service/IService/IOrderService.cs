using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.OrderModel;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.IService
{
    public interface IOrderService
    {
        public Task<ResponseModel> GetAllAsync();
        public Task<ResponseModel> GetByIdAsync(int id);
        public Task<ResponseModel> CreateOrderAsync(RequestCreateOrder requestOrder);
        public Task<ResponseModel> UpdateOrderAsync(int orderId, RequestUpdateOrder requestOrder);
        public bool IsValidOrderType(string? input);

    }
}