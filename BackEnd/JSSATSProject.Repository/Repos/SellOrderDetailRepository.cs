using JSSATSProject.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace JSSATSProject.Repository.Repos;

public class SellOrderDetailRepository : GenericRepository<SellOrderDetail>
{
    public SellOrderDetailRepository(DBContext context) : base(context)
    {
    }

    public async Task<SellOrderDetail?> GetEntityByIdAsync(int id)
    {
        return await context.SellOrderDetails
            .Where(s => s.Id == id)
            .Include(s => s.Product)
            .FirstOrDefaultAsync();
    }
}