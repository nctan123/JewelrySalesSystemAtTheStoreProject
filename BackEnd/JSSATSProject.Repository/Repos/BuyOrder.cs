using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class BuyOrder : GenericRepository<BuyOrder>
{
    public BuyOrder(DBContext context) : base(context)
    {
    }
}