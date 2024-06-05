using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.ProductModel;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.IService
{
    public interface IProductService
    {
        public Task<ResponseModel> GetAllAsync();
        public Task<ResponseModel> GetByIdAsync(int id);
        public Task<ResponseModel> GetByNameAsync(string name);
        public Task<ResponseModel> GetByCodeAsync(string code);
        public Task<ResponseModel> CreateProductAsync(RequestCreateProduct requestProduct);
        public Task<ResponseModel> UpdateProductAsync(int productId, RequestUpdateProduct requestProduct);
    }
}