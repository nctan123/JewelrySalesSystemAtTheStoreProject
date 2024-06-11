using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JSSATSProject.Service.Models.OrderDetail;

namespace JSSATSProject.Service.Service.IService
{
    public interface IOrderDetailService
    {

        public Task<ResponseModel> GetByOrderIdAsync(int id);
        public Task<ResponseModel> CreateOrderDetailAsync(RequestCreateOrderDetail requestOrderDetail);
        public Task<ResponseModel> UpdateOrderDetailAsync(int orderdetailId, RequestUpdateOrderDetail requestOrderDetail);
    }
}
