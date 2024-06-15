using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.OrderModel;

namespace JSSATSProject.Service.Service.IService;

public interface IBuyOrderService
{
    public Task<ResponseModel> GetAllAsync();
    public Task<ResponseModel> GetByIdAsync(int id);
    public Task<ResponseModel> GetByProductIdAsync(int productId);
    public Task<ResponseModel> CreateAsync(RequestCreateBuyOrder entity);
}