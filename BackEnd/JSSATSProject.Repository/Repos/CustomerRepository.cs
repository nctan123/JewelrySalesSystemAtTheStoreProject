using JSSATSProject.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace JSSATSProject.Repository.Repos;

public class CustomerRepository : GenericRepository<Customer>
{
    public CustomerRepository(DBContext context) : base(context)
    {
      
    }

    public async Task<Account?> GetByUsernameAndPassword(string username, string password)
    {
        return await context.Accounts
            .Include(a => a.Role)
            .FirstOrDefaultAsync(a => a.Username == username && a.Password == password);
    }
}