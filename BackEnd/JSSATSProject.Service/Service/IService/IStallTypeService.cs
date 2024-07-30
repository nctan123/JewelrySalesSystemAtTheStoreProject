using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.StallTypeModel;

namespace JSSATSProject.Service.Service.IService;

public interface IStallTypeService
{
    public Task<ResponseModel> GetAllAsync();
    public Task<ResponseModel> GetByIdAsync(int id);
    public Task<ResponseModel> CreateStallTypeAsync(RequestCreateStallType requestStallType);
}