using JSSATSProject.Service.Models.GuaranteeModel;
using JSSATSProject.Service.Models;
using System.Threading.Tasks;
using JSSATSProject.Service.Models.ReturnBuyBackPolicyModel;

namespace JSSATSProject.Service.Service.IService
{
    public interface IReturnBuyBackPolicyService
    {
        public Task<ResponseModel> GetAllAsync();
        public Task<ResponseModel> GetByIdAsync(int id);
        public Task<ResponseModel> CreateReturnBuyBackPolicyAsync(RequestCreateReturnBuyBackPolicy requestReturnBuyBackPolicy);
        public Task<ResponseModel> UpdateReturnBuyBackPolicyAsync(int Id, RequestUpdateReturnBuyBackPolicy requestReturnBuyBackPolicy);
    }
}