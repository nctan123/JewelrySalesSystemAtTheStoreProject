using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class SellOrderDetailRepository : GenericRepository<SellOrderDetail>
{
    public SellOrderDetailRepository(DBContext context) : base(context)
    {
    }
}