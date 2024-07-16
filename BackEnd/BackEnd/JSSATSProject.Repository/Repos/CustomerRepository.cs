using JSSATSProject.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace JSSATSProject.Repository.Repos;

public class CustomerRepository : GenericRepository<Customer>
{
    public CustomerRepository(DBContext context) : base(context)
    {
    }

    public async Task<Account?> GetByUsernameAndPasswordAsync(string username, string password)
    {
        return await context.Accounts
            .Include(a => a.Role)
            .FirstOrDefaultAsync(a => a.Username == username && a.Password == password);
    }

    public async Task<Customer?> GetEntityByIdAsync(int customerId)
    {
        return await context.Customers
            .Include(c => c.SellOrders)
            .ThenInclude(so => so.SpecialDiscountRequest)
            .Include(c => c.SellOrders)
            .Include(c => c.BuyOrders)
            .Include(c => c.Payments)
            .FirstOrDefaultAsync(c => c.Id == customerId);
    }

    public async Task<Dictionary<DateTime, int>> GetCustomersByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        var result = new Dictionary<DateTime, int>();
        var customerCountByDate = await context.Customers
            .Where(c => c.CreateDate >= startDate && c.CreateDate <= endDate)
            .GroupBy(c => c.CreateDate)
            .Select(g => new { Date = g.Key.Date, Count = g.Count() })
            .ToListAsync();

        foreach (var customer in customerCountByDate)
        {
            var key = customer.Date.Date;
            if (result.ContainsKey(key))
            {
                result[key] += 1;
            }
            else
            {
                result.Add(customer.Date, customer.Count);
            }
        }

        return result;
    }
}