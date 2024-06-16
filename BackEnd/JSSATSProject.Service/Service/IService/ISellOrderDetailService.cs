using JSSATSProject.Service.Models;
using JSSATSProject.Repository.Entities;


namespace JSSATSProject.Service.Service.IService
{
    public interface ISellOrderDetailService
    {
        public Task<ResponseModel> CountProductsSoldByCategoryAsync(DateTime startDate, DateTime endDate);

        public Task<List<SellOrderDetail>> GetAllEntitiesFromSellOrderAsync(int sellOrderId,
            Dictionary<string, int> productCodesAndQuantity, Dictionary<string, string> productCodesAndPromotionIds);

        public Task UpdateAllOrderDetailsStatus(SellOrder order, string newStatus);
    }
}