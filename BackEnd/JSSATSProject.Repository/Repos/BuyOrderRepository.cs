using JSSATSProject.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JSSATSProject.Repository.Repos;

public class BuyOrderRepository : GenericRepository<BuyOrder>
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

    public async Task<BuyOrder> GetEntityAsync(int id)
    {
        return await context.BuyOrders
            .Where(b => b.Id == id)
            .Include(b => b.Customer)
            .Include(b => b.BuyOrderDetails)
            .Include(b => b.Staff)
            .FirstAsync();
    }

    public async Task<decimal> SumAsync(Expression<Func<BuyOrder, bool>> filter,
        Expression<Func<BuyOrder, decimal>> selector)
    {
        IQueryable<BuyOrder> query = dbSet;

        if (filter != null) query = query.Where(filter);

        return await query.SumAsync(selector);
    }
}