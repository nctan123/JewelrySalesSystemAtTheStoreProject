using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.ProductCategoryTypeModel;

namespace JSSATSProject.Service.Service.IService;

public interface IProductCategoryTypeService
{
    public Task<ResponseModel> GetAllAsync();
    public Task<ResponseModel> GetByIdAsync(int id);

    public Task<ResponseModel> CreateProductCategoryTypeAsync(
        RequestCreateProductCategoryType requestProductCategoryType);
}