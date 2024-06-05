using JSSATSProject.Service.Models.GuaranteeModel;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.PaymentModel;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.IService
{
    public interface IPaymentService
    {
        public Task<ResponseModel> GetAllAsync();
        public Task<ResponseModel> GetByIdAsync(int id);
        public Task<ResponseModel> CreatePaymentAsync(RequestCreatePayment requestPayment);
        public Task<ResponseModel> UpdatePaymentAsync(int paymentId, RequestUpdatePayment requestPayment);
    }
}