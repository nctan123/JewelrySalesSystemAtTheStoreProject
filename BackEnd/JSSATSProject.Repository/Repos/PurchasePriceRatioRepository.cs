using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class PurchasePriceRatioRepository : GenericRepository<PurchasePriceRatio>
{
    public PurchasePriceRatioRepository(DBContext context) : base(context)
    {
    }
}