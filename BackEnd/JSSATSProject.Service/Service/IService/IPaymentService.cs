using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.PaymentModel;

namespace JSSATSProject.Service.Service.IService;

public interface IPaymentService
{
    public Task<ResponseModel> GetAllAsync(int pageIndex, int pageSize);
    public Task<ResponseModel> GetByIdAsync(int id);
    public Task<ResponseModel> CreatePaymentAsync(RequestCreatePayment requestPayment);
    public Task<ResponseModel> UpdatePaymentAsync(int paymentId, RequestUpdatePayment requestPayment);
    public Task<int?> GetSellOrderIdByPaymentIdAsync(int id);
    public Task<ResponseModel> GetTotalAllPayMentAsync(DateTime startDate, DateTime endDate, int order);
    public Task<int?> GetBuyOrderIdByPaymentIdAsync(int id);
    Task<ResponseModel> GetByOrderCodeAsync(string orderCode);
}
