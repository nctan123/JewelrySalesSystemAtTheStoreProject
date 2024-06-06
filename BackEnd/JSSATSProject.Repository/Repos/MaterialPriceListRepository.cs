using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class MaterialPriceListRepository : GenericRepository<MaterialPriceList>
{
    public MaterialPriceListRepository(DBContext context) : base(context)
    {
    }
}