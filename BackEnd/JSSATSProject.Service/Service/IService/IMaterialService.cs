using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.Material;

namespace JSSATSProject.Service.Service.IService;

public interface IMaterialService
{
    Task<ResponseModel> GetAllAsync();
    Task<ResponseModel> GetByIdAsync(int id);
    Task<ResponseModel> CreateMaterialAsync(RequestCreateMaterial requestMaterial);
}