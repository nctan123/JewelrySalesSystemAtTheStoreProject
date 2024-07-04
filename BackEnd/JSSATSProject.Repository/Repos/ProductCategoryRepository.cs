using JSSATSProject.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace JSSATSProject.Repository.Repos;

public class ProductCategoryRepository : GenericRepository<ProductCategory>
{
    public ProductCategoryRepository(DBContext context) : base(context)
    {

    }

    public async Task<int> GetTypeIdByCategoryIdAsync(int categoryId)
    {
        var category = await context.ProductCategories
            .FirstOrDefaultAsync(pc => pc.Id == categoryId);

        return category?.TypeId ?? 0; // Return 0 if the category is not found
    }
}