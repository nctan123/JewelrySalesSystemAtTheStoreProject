using JSSATSProject.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace JSSATSProject.Repository.Repos;

public class BuyOrderRepository : GenericRepository<BuyOrderRepository>
{
    public BuyOrderRepository(DBContext context) : base(context)
    {
    }

    public Task<BuyOrder?> GetByCodeAsync(string code)
    {
        return context.BuyOrders
            .Where(b => b.Code == code)
            .Include(b => b.BuyOrderDetails)
            .Include(b => b.Customer)
            .Include(b => b.Staff)
            .FirstOrDefaultAsync();
    }
}