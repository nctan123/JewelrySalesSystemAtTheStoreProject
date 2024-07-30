using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class ProductRepository : GenericRepository<Product>
{
    public ProductRepository(DBContext context) : base(context)
    {
    }
}