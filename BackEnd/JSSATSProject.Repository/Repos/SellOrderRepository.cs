using JSSATSProject.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JSSATSProject.Repository.Repos;

public class SellOrderRepository : GenericRepository<SellOrder>
{
    public SellOrderRepository(DBContext context) : base(context)
    {
    }

    public async Task<SellOrder?> GetEntityByIdAsync(int id)
    {
        return await context.SellOrders.Where(o => o.Id == id)
            .Include(o => o.SellOrderDetails)
            .FirstOrDefaultAsync();
    }
    
    public async Task<decimal> SumAsync(Expression<Func<SellOrder, bool>> filter, Expression<Func<SellOrder, decimal>> selector)
    {
        IQueryable<SellOrder> query = dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }
        return await query.SumAsync(selector);
    }
}