using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.DiamondModel;

namespace JSSATSProject.Service.Service.IService;

public interface IDiamondService
{
    public Task<ResponseModel> GetAllAsync();
    public Task<ResponseModel> GetByIdAsync(int id);
    public Task<ResponseModel> GetByNameAsync(string name);
    public Task<ResponseModel> GetByCodeAsync(string code);
    public Task<ResponseModel> CreateDiamondAsync(RequestCreateDiamond requestDiamond);
    public Task<ResponseModel> UpdateDiamondAsync(int diamondId, RequestUpdateDiamond requestDiamond);
    public Task<ResponseModel> UpdateStatusDiamondAsync(int diamondId, RequestUpdateStatusDiamond requestDiamond);
}