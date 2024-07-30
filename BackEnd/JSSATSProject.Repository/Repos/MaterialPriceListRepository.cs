using JSSATSProject.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace JSSATSProject.Repository.Repos;

public class MaterialPriceListRepository : GenericRepository<MaterialPriceList>
{
    public MaterialPriceListRepository(DBContext context) : base(context)
    {
    }

    public async Task<decimal> GetMaterialBuyPriceAsync(int? materialId, decimal? materialWeight)
    {
        var materialPriceList = await context.MaterialPriceLists
            .Where(m => m.MaterialId == materialId)
            .OrderByDescending(m => m.EffectiveDate)
            .FirstOrDefaultAsync();
        var price = materialPriceList!.BuyPrice * materialWeight;
        return price.GetValueOrDefault();
    }
}