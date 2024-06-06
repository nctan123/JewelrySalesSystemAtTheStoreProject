using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class OrderDetailRepository : GenericRepository<OrderDetail>
{
    public OrderDetailRepository(DBContext context) : base(context)
    {
    }
}