using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class ProductCategoryTypeRepository : GenericRepository<ProductCategoryType>
{
    public ProductCategoryTypeRepository(DBContext context) : base(context)
    {
    }
}