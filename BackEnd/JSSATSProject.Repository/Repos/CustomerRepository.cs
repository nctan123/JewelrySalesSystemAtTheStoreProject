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

}