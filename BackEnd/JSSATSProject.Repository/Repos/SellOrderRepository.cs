using System.Linq.Expressions;
using JSSATSProject.Repository.Entities;
using Microsoft.EntityFrameworkCore;

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
            .ThenInclude(sd => sd.Promotion)
            .Include(o => o.Customer)
            .Include(o => o.SpecialDiscountRequest)
            .Include(o => o.Customer.Point)
            .FirstOrDefaultAsync();
    }

    public async Task<decimal> SumAsync(Expression<Func<SellOrder, bool>> filter,
        Expression<Func<SellOrder, decimal>> selector)
    {
        IQueryable<SellOrder> query = dbSet;

        if (filter != null) query = query.Where(filter);

        return await query.SumAsync(selector);
    }

    public async Task<SellOrder?> GetByCodeAsync(string code)
    {
        var result = await context.SellOrders
            .Where(s => s.Code == code)
            .Include(s => s.SellOrderDetails)
            .ThenInclude(detail => detail.Product)
            .ThenInclude(product => product.ProductMaterials)
            .ThenInclude(pm => pm.Material)
            .ThenInclude(m => m.MaterialPriceLists)
            .Include(s => s.SellOrderDetails)
            .ThenInclude(detail => detail.Product)
            .ThenInclude(product => product.ProductDiamonds)
            .ThenInclude(pd => pd.Diamond)
            .Include(s => s.Payments)
            .Include(s => s.Customer)
            .Include(s => s.Staff)
            .Include(s => s.SpecialDiscountRequest)
            .FirstOrDefaultAsync();
        return result;
    }

    public async Task<Dictionary<DateTime, int>> GetOrdersByDateRange(DateTime startDate, DateTime endDate)
    {
        var result = new Dictionary<DateTime, int>();
        var ordersCountByDate = await context.SellOrders
            .Where(c => c.CreateDate >= startDate && c.CreateDate <= endDate)
            .GroupBy(c => c.CreateDate)
            .Select(g => new { Date = g.Key.Date, Count = g.Count() })
            .ToListAsync();

        foreach (var sellOrders in ordersCountByDate)
        {
            var key = sellOrders.Date.Date;
            if (result.ContainsKey(key))
            {
                result[key] += 1;
            }
            else
            {
                result.Add(sellOrders.Date, sellOrders.Count);
            }
        }

        return result;
    }

    public async Task<Dictionary<DateTime, decimal>> GetTotalAmountByDateRange(DateTime startDate, DateTime endDate)
    {
        var result = new Dictionary<DateTime, decimal>();
        var ordersCountByDate = await context.SellOrders
            .Where(c => c.CreateDate >= startDate && c.CreateDate <= endDate)
            .GroupBy(c => c.CreateDate)
            .Select(g => new { g.Key.Date, Total = g.Select(g => g.TotalAmount).Sum() })
            .ToListAsync();

        foreach (var sellOrders in ordersCountByDate)
        {
            var key = sellOrders.Date.Date;
            if (result.ContainsKey(key))
            {
                result[key] += 1;
            }
            else
            {
                result.Add(sellOrders.Date, sellOrders.Total);
            }
        }

        return result;
    }
}