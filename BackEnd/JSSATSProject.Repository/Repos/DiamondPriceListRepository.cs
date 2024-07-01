using JSSATSProject.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace JSSATSProject.Repository.Repos;

public class DiamondPriceListRepository : GenericRepository<DiamondPriceList>
{
    public DiamondPriceListRepository(DBContext context) : base(context)
    {
    }

    public async Task<decimal> FindPriceBy4CAndOrigin(int cutId, int clarityId, int colorId, int caratId, int originId,
        DateTime closestDate)
    {
        var matchesDiamondPriceObject = await context.DiamondPriceLists.Where(d => d.CutId == cutId
                && d.CaratId == caratId
                && d.ClarityId == clarityId
                && d.ColorId == colorId
                && d.OriginId == originId
                && d.EffectiveDate.Date == closestDate.Date)
            .FirstOrDefaultAsync();
        return matchesDiamondPriceObject!.Price;
    }

    public async Task<DateTime> GetClosestPriceEffectiveDate(DateTime timeStamp)
    {
        var entity = await context.DiamondPriceLists.OrderByDescending(d => d.EffectiveDate)
            .Where(d => d.EffectiveDate <= timeStamp)
            .FirstAsync();
        return entity.EffectiveDate;
    }
}