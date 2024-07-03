using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.ReturnBuyBackPolicyModel;

namespace JSSATSProject.Service.Service.IService;

public interface IReturnBuyBackPolicyService
{
    public Task<ResponseModel> GetAllAsync(int pageIndex, int pageSize, bool ascending);
    public Task<ResponseModel> GetByIdAsync(int id);

    public Task<ResponseModel> CreateReturnBuyBackPolicyAsync(
        RequestCreateReturnBuyBackPolicy requestReturnBuyBackPolicy);

    public Task<ResponseModel> UpdateReturnBuyBackPolicyAsync(int Id,
        RequestUpdateReturnBuyBackPolicy requestReturnBuyBackPolicy);
}