using JSSATSProject.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace JSSATSProject.Repository.Repos;

public class GuaranteeRepository : GenericRepository<Guarantee>
{
    public GuaranteeRepository(DBContext context) : base(context)
    {
    }

    public async Task<Guarantee> GetByProductIdAsync(int productId)
    {
        return await context.Guarantees
            .FirstOrDefaultAsync(g => g.ProductId == productId);
    }

    public async Task<Guarantee?> GetEntityByCodeAsync(string guaranteeCode)
    {
        var entity = await context.Guarantees
            .Where(g => g.Code == guaranteeCode)
            .Include(g => g.SellOrderDetail)
            .ThenInclude(sd => sd.Order)
            .ThenInclude(s => s.Customer)
            .Include(s => s.Product)
            .FirstOrDefaultAsync();
        return entity;
    }
}