using System.Linq.Expressions;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.BuyOrderModel;

namespace JSSATSProject.Service.Service.IService;

public interface IBuyOrderService
{
    public Task<ResponseModel> GetAllAsync(List<string> statusList, bool ascending, int pageIndex, int pageSize);
    public Task<ResponseModel> GetByIdAsync(int id);
    public Task<ResponseModel> CreateAsync(BuyOrder entity);

    public Task<ResponseModel> UpdateAsync(int buyOrderId, BuyOrder entity);
    
    public Task<ResponseModel> UpdateAsync(int buyOrderId, RequestUpdateBuyOrderStatus entity);

    public decimal GetPrice(string targetProductCode, Dictionary<string, int> productCodesAndQuantity,
        Dictionary<string, int> productCodesAndEstimatePrices);

    public decimal GetTotalAmount(Dictionary<string, int> productCodesAndQuantity,
        Dictionary<string, decimal> productCodesAndEstimatePrices);

    public Task<ICollection<BuyOrderDetail>> CreateOrderDetails(RequestCreateBuyOrder requestCreateBuyOrder,
        int buyOrderId);

    public Task<ICollection<BuyOrderDetail>> CreateOrderDetails(RequestCreateNonCompanyBuyOrder requestCreateBuyOrder,
        int buyOrderId);

    Task<int?> CountAsync(Expression<Func<BuyOrder, bool>> filter);

    Task<ResponseModel> SearchByCriteriaAsync(List<string> statusList, string customerPhone, bool ascending,
        int pageIndex, int pageSize);
}