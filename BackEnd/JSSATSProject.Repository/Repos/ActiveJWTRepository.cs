using JSSATSProject.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace JSSATSProject.Repository.Repos;

public class ActiveJWTRepository : GenericRepository<ActiveJwt>
{
    public ActiveJWTRepository(DBContext context) : base(context)
    {
    }

    public async Task<bool> IsValidTokenAsync(string token)
    {
        return await context.ActiveJwts.AnyAsync(a => a.Token == token);
    }
}