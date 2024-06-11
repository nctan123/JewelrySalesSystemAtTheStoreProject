using JSSATSProject.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace JSSATSProject.Repository.Repos;

public class CustomerRepository : GenericRepository<Customer>
{
    public CustomerRepository(DBContext context) : base(context)
    {
    }

    public async Task<Customer?> FindByPhoneNumber(string phoneNumberStr)
    {
        var customer = await context.Customers
            .FirstOrDefaultAsync(c => c.Phone.Equals(phoneNumberStr));
        return customer;
    }
}