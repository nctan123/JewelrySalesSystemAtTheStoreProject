using JSSATSProject.Service.Models;
using JSSATSProject.Repository.Entities;


namespace JSSATSProject.Service.Service.IService
{
    public interface ISellOrderDetailService
    {
        public Task<ResponseModel> CountProductsSoldByCategoryAsync(DateTime startDate, DateTime endDate);

        public Task<List<SellOrderDetail>> GetAllEntitiesFromSellOrderAsync(int sellOrderId,
            Dictionary<string, int> productCodesAndQuantity, Dictionary<string, int?>? productCodesAndPromotionIds);

        public Task UpdateAllOrderDetailsStatus(SellOrder order, string newStatus);

        public Task<ResponseModel> GetTotalRevenueStallAsync(DateTime startDate, DateTime endDate);
    }
}