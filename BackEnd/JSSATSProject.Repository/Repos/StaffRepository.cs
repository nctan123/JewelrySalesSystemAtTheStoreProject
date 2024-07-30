using JSSATSProject.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace JSSATSProject.Repository.Repos;

public class StaffRepository : GenericRepository<Staff>
{
    public StaffRepository(DBContext context) : base(context)
    {
    }
    public async Task<Staff> GetByIDAsync(int id)
    {
        return await context.Staff
            .Include(s => s.Account)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}