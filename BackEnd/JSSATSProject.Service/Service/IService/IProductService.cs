using System.Linq.Expressions;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.ProductModel;

namespace JSSATSProject.Service.Service.IService;

public interface IProductService
{
    public Task<ResponseModel> GetByCodeAsync(string code);
    public Task<Product> GetEntityByCodeAsync(string code);
    public Task<decimal> CalculateProductPrice(Product correspondingProduct, int quantity);
    public Task<decimal> CalculateProductPrice(Product responseProduct);
    public Task<ResponseModel> CreateProductAsync(RequestCreateProduct requestProduct);
    public Task<ResponseModel> UpdateStallProductAsync(int productId, RequestUpdateProduct requestProduct);

    public Task<ResponseModel> GetAllAsync(int categoryId, int pageIndex = 1, int pageSize = 10, bool ascending = true, bool includeNullStalls = true);

    public Task<ResponseModel> SearchProductsAsync(int categoryId, string searchTerm, int pageIndex = 1, int pageSize = 10, bool ascending = true, bool includeNullStalls = true);

    public Task<int> CountAsync(Expression<Func<Product, bool>> filter = null);
}