using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class ProductMaterialRepository : GenericRepository<ProductMaterial>
{
    public ProductMaterialRepository(DBContext context) : base(context)
    {
    }
}