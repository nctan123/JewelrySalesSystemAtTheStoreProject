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
<<<<<<< HEAD

        public Task<bool> AreValidProducts(Dictionary<string, int> productCodes);
=======
        public Task<ResponseModel> UpdateStatusProductAsync(int productId, RequestUpdateStatusProduct requestProduct);
>>>>>>> ef1d898c610203bb40990ce34f1644abc601b704
    }
}