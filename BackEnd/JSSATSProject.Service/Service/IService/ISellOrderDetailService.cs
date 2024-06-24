using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models.OrderDetail;
using JSSATSProject.Service.Models.ProductModel;

namespace JSSATSProject.Service.Service.IService
{
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

        public Task<ResponseModel> GetTotalRevenueStallAsync(DateTime startDate, DateTime endDate);

        public Task<List<ResponseProductDetails>> GetProductFromSellOrderDetailAsync(int orderId);
        
    }
}