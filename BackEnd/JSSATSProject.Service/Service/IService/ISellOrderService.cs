using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.OrderModel;
using System.Threading.Tasks;
using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Service.Service.IService
{
    public interface ISellOrderService
    {
        public Task<ResponseModel> GetAllAsync();
        public Task<ResponseModel> CountOrderByDateTimeAsync(DateTime startDate, DateTime endDate);
        public Task<ResponseModel> SumTotalAmountOrderByDateTimeAsync(DateTime startDate, DateTime endDate);
        public Task<ResponseModel> GetByIdAsync(int id);
        public Task<SellOrder> GetEntityByIdAsync(int id);
        public Task<ResponseModel> CreateOrderAsync(RequestCreateSellOrder requestSellOrder);
        public Task<ResponseModel> UpdateOrderAsync(int orderId, RequestUpdateSellOrder requestSellOrder);
        public Task<ResponseModel> UpdateStatusAsync(int orderId, UpdateSellOrderStatus requestSellOrder);
    }
}