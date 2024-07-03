using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.PurchasePriceRatioModel;

namespace JSSATSProject.Service.Service.IService;

public interface IPurchasePriceRatioService
{
    public Task<ResponseModel> CreateAsync(RequestCreatePurchasePriceRatio requestCreatePurchasePriceRatio);
    public Task<ResponseModel> GetAllAsync();
    public Task<ResponseModel> GetByIdAsync(int id);
}