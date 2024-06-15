using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;

namespace JSSATSProject.Service.Service.IService;

public interface IPurchasePriceRatioService
{
    public Task<ResponseModel> GetAllAsync();
    public Task<ResponseModel> GetByIdAsync(int id);
}