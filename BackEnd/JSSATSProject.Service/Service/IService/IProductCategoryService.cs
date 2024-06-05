using JSSATSProject.Service.Models.GuaranteeModel;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.ProductCategoryModel;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.IService
{
    public interface IProductCategoryService
    {
        public Task<ResponseModel> GetAllAsync();
        public Task<ResponseModel> GetByIdAsync(int id);
        public Task<ResponseModel> CreateProductCategoryAsync(RequestCreateProductCategory requestProductCategory);
    }
}