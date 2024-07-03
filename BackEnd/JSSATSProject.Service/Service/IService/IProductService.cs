using System.Linq.Expressions;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.ProductModel;

namespace JSSATSProject.Service.Service.IService;

public interface IProductService
{
    public Task<ResponseModel> GetAllAsync();
    public Task<ResponseModel> GetByCodeAsync(string code);
    public Task<Product> GetEntityByCodeAsync(string code);
    public Task<decimal> CalculateProductPrice(Product correspondingProduct, int quantity);
    public Task<decimal> CalculateProductPrice(Product responseProduct);
    public Task<ResponseModel> CreateProductAsync(RequestCreateProduct requestProduct);
    public Task<ResponseModel> UpdateProductAsync(int productId, RequestUpdateProduct requestProduct);
    public Task<ResponseModel> UpdateProductStatusAsync(int productId, string newStatus);
    public Task UpdateAllProductStatusAsync(SellOrder sellOrder, string newStatus);

    /// Update
    public Task<ResponseModel> GetAllAsync(int categoryId, int pageIndex = 1, int pageSize = 10, bool ascending = true);

    public Task<ResponseModel> SearchProductsAsync(int categoryId, string searchTerm, int pageIndex = 1,
        int pageSize = 10);

    public Task<int> CountAsync(Expression<Func<Product, bool>> filter = null);
    public Task<decimal> CalculateMaterialBuyPrice(int? materialId, decimal? materialWeight);
}