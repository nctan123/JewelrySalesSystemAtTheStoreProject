using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.ProductModel;
using System.Threading.Tasks;
using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Service.Service.IService
{
    public interface IProductService
    {
        public Task<ResponseModel> GetAllAsync();
        public Task<ResponseModel> GetByCodeAsync(string code);
        public Task<Product> GetEntityByCodeAsync(string code);
        public Task<decimal> CalculateProductPrice(Product correspondingProduct, int quantity);
        public Task<decimal> CalculateProductPrice(Product responseProduct);
        public Task<ResponseModel> CreateProductAsync(RequestCreateProduct requestProduct);
        public Task<ResponseModel> UpdateProductAsync(int productId, RequestUpdateProduct requestProduct);

        /// Update
        public Task<ResponseModel> GetFilteredAndSortedProductsAsync(int categoryId, int pageIndex = 1, int pageSize = 10, bool ascending = true);

        public Task<ResponseModel> SearchProductsAsync(int categoryId, string searchTerm, int pageIndex = 1, int pageSize = 10);
    }
}