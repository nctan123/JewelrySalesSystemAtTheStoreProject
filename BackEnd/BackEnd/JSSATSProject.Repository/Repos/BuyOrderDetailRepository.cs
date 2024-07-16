using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class BuyOrderDetailRepository : GenericRepository<BuyOrderDetail>
{
    public BuyOrderDetailRepository(DBContext context) : base(context)
    {
    }
}