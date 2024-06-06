using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class ProductCategoryRepository : GenericRepository<ProductCategory>
{
    public ProductCategoryRepository(DBContext context) : base(context)
    {
    }
}