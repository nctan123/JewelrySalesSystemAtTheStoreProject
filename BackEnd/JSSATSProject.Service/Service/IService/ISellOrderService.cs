using System.Linq.Expressions;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.OrderModel;
using JSSATSProject.Service.Models.ProductModel;
using JSSATSProject.Service.Models.SellOrderModel;

namespace JSSATSProject.Service.Service.IService;

public interface ISellOrderService
{
    public Task<ResponseModel> GetAllAsync(List<string> statusList, bool ascending = true, int pageIndex = 1,
        int pageSize = 10);

    public Task<ResponseModel> CountOrderByDateTimeAsync(DateTime startDate, DateTime endDate);
    public Task<int> CountAsync(Expression<Func<SellOrder, bool>> filter = null);
    public Task<ResponseModel> SumTotalAmountOrderByDateTimeAsync(DateTime startDate, DateTime endDate);
    public Task<ResponseModel> GetByIdAsync(int id);
    public Task<SellOrder> GetEntityByIdAsync(int id);
    public Task<SellOrder?> GetEntityByCodeAsync(string code);
    public Task<ResponseModel> CreateOrderAsync(RequestCreateSellOrder requestSellOrder);
    public Task<ResponseModel> UpdateOrderAsync(int orderId, RequestUpdateSellOrder requestSellOrder);
    public Task<ResponseModel> UpdateStatusAsync(int orderId, UpdateSellOrderStatus requestSellOrder);
    public Task<List<ResponseProductForCheckOrder>> GetProducts(SellOrder? sellOrder);

    public Task<ResponseModel> SearchByAsync(List<string> statusList, string customerPhone,
        bool ascending = true, int pageIndex = 1, int pageSize = 10);

    public Task RemoveAllSellOrderDetails(int id);

    public Task<SellOrder> MapOrderAsync(RequestCreateSellOrder requestSellOrder);

    public Task<ResponseModel> UpdateOrderAsync(int orderId, SellOrder targetOrder);

    public Task<decimal> GetFinalPriceAsync(SellOrder sellOrder);

}