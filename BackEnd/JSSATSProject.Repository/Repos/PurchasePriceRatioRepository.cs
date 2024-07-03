using JSSATSProject.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace JSSATSProject.Repository.Repos;

public class PurchasePriceRatioRepository : GenericRepository<PurchasePriceRatio>
{
    public PurchasePriceRatioRepository(DBContext context) : base(context)
    {
    }

    public async Task<decimal> GetRate(int categoryTypeId)
    {
        var rate = await context.PurchasePriceRatios
            .Where(p => p.CategoryTypeId == categoryTypeId && p.Returnbuybackpolicy.Status == "active")
            .FirstOrDefaultAsync();
        return rate?.Percentage ?? 1;
    }

    public async Task<PurchasePriceRatio> GetEntity(int categoryTypeId, string type)
    {
        var rate = await context.PurchasePriceRatios
            .Where(p => p.CategoryTypeId == categoryTypeId 
                        && p.Returnbuybackpolicy.Status == "active"
                        && p.Type == type
                        )
            .FirstOrDefaultAsync();
        return rate;
    }
}