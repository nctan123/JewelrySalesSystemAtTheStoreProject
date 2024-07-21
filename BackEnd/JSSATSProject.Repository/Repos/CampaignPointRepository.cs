using JSSATSProject.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace JSSATSProject.Repository.Repos;

public class CampaignPointRepository : GenericRepository<CampaignPoint>
{
    public CampaignPointRepository(DBContext context) : base(context)
    {
    }

    public async Task<decimal> GetPointRate(DateTime timeStamp)
    {
        var rate = await context.CampaignPoints
            .Where( c => EF.Functions.Like(c.Description, "Point value to VND") && c.StartDate <= timeStamp && c.EndDate >= timeStamp)
            .Select(c => c.Rate)
            .FirstOrDefaultAsync();
        return rate!.Value;
    }

    public async Task<decimal> GetOrderValueToPointConversionRate(DateTime timeStamp)
    {
        var rate = await context.CampaignPoints
            .Where(c => EF.Functions.Like(c.Description, "Order value to point") && c.StartDate <= timeStamp &&
                        (c.EndDate == null || c.EndDate >= timeStamp))
            .Select(c => c.Rate)
            .FirstOrDefaultAsync();
        var check = rate;
        if (rate == null) throw new InvalidOperationException("No valid rate found for the given timestamp.");
        return rate.Value;
    }
}