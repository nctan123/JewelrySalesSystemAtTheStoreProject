using JSSATSProject.Service.Models.GuaranteeModel;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.PaymentMethodModel;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.IService
{
    public interface IPaymentMethodService
    {
        public Task<ResponseModel> GetAllAsync();
        public Task<ResponseModel> GetByIdAsync(int id);
        public Task<ResponseModel> CreatePaymentMethodAsync(RequestCreatePaymentMethod requestPaymentMethod);
        public Task<ResponseModel> UpdatePaymentMethodAsync(int paymentmethodId, RequestUpdatePaymentMethod requestPaymentMethod);
    }
}