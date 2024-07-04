using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.PaymentModel;

namespace JSSATSProject.Service.Service.IService;

public interface IPaymentService
{
    public Task<ResponseModel> GetAllAsync(int pageIndex, int pageSize);
    public Task<ResponseModel> GetByIdAsync(int id);
    public Task<ResponseModel> CreatePaymentAsync(RequestCreatePayment requestPayment);
    public Task<ResponseModel> UpdatePaymentAsync(int paymentId, RequestUpdatePayment requestPayment);
    public Task<int> GetOrderIdByPaymentIdAsync(int id);
    public Task<decimal> GetTotal(int paymentMethod, DateTime startDate, DateTime endDate);

}