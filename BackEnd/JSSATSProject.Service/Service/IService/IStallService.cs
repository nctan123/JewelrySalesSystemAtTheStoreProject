using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.StallModel;

namespace JSSATSProject.Service.Service.IService;

public interface IStallService
{
    public Task<ResponseModel> GetAllAsync();
    public Task<ResponseModel> GetByIdAsync(int id);
    public Task<ResponseModel> CreateStallAsync(RequestCreateStall requestStall);
}