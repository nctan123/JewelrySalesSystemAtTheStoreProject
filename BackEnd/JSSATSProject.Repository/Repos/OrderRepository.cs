using JSSATSProject.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JSSATSProject.Repository.Repos;

public class OrderRepository : GenericRepository<Order>
{
    public OrderRepository(DBContext context) : base(context)
    {
    }

    public async Task<decimal> SumAsync(Expression<Func<Order, bool>> filter, Expression<Func<Order, decimal>> selector)
    {
        IQueryable<Order> query = dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }
        return await query.SumAsync(selector);
    }
}