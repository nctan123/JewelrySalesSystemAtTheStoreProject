using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.OrderModel;
using System.Threading.Tasks;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models.ProductModel;

namespace JSSATSProject.Service.Service.IService
{
    public interface ISellOrderService
    {
        public Task<ResponseModel> GetAllAsync(bool ascending, int pageIndex = 1, int pageSize = 10);
        public Task<ResponseModel> CountOrderByDateTimeAsync(DateTime startDate, DateTime endDate);
        public Task<ResponseModel> SumTotalAmountOrderByDateTimeAsync(DateTime startDate, DateTime endDate);
        public Task<ResponseModel> GetByIdAsync(int id);
        public Task<SellOrder> GetEntityByIdAsync(int id);
        public Task<SellOrder?> GetEntityByCodeAsync(string code);
        public Task<ResponseModel> CreateOrderAsync(RequestCreateSellOrder requestSellOrder);
        public Task<ResponseModel> UpdateOrderAsync(int orderId, RequestUpdateSellOrder requestSellOrder);
        public Task<ResponseModel> UpdateStatusAsync(int orderId, UpdateSellOrderStatus requestSellOrder);
        public Task<List<ResponseProductForCheckOrder>> GetProducts(SellOrder? sellOrder);
    }
}