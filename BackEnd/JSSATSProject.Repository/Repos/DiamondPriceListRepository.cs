using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class DiamondPriceListRepository : GenericRepository<DiamondPriceList>
{
    public DiamondPriceListRepository(DBContext context) : base(context)
    {
    }
}