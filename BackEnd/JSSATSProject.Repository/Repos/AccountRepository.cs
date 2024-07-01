using JSSATSProject.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace JSSATSProject.Repository.Repos;

public class AccountRepository : GenericRepository<Account>
{
    public AccountRepository(DBContext context) : base(context)
    {
    }

    public async Task<Account?> GetByUsernameAndPassword(string username, string password)
    {
        return await context.Accounts
            .Include(a => a.Role)
            .Include(a => a.Staff)
            .FirstOrDefaultAsync(a => a.Username == username && a.Password == password);
    }
}