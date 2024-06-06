using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class OrderRepository : GenericRepository<Order>
{
    public OrderRepository(DBContext context) : base(context)
    {
    }
}