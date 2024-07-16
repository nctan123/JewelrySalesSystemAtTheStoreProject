using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.OrderDetail;
using JSSATSProject.Service.Models.ProductModel;

namespace JSSATSProject.Service.Service.IService;

public interface ISellOrderDetailService
{
    public Task<ResponseModel> GetByOrderIdAsync(int id);
    public Task<ResponseModel> CreateOrderDetailAsync(RequestCreateOrderDetail requestOrderDetail);

    public Task<ResponseModel> UpdateOrderDetailAsync(int orderdetailId,
        RequestUpdateOrderDetail requestOrderDetail);

    public Task<ResponseModel> CountProductsSoldByCategoryAsync(DateTime startDate, DateTime endDate);

    public Task<List<SellOrderDetail>> GetAllEntitiesFromSellOrderAsync(int sellOrderId,
        Dictionary<string, int> productCodesAndQuantity, Dictionary<string, int?>? productCodesAndPromotionIds);

    public Task UpdateAllOrderDetailsStatus(SellOrder order, string newStatus);

    public Task<List<ResponseProductDetails>> GetProductFromSellOrderDetailAsync(int orderId);

    public Task<ResponseModel> GetProductSoldAsync(bool ascending, int pageIndex, int pageSize);

    public Task<ResponseModel> GetProductsByStallAsync(int stallId, DateTime startDate, DateTime endDate,
        int pageIndex, int pageSize, bool ascending);
}