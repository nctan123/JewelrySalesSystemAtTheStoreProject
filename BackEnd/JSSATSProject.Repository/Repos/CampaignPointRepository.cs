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
            .Where(c => c.StartDate >= timeStamp && c.EndDate <= timeStamp)
            .Select(c => c.Rate)
            .FirstOrDefaultAsync();
        return rate ?? 1;
    }
    
    public async Task<decimal> GetOrderValueToPointConversionRate(DateTime timeStamp)
    {
        var rate = await context.CampaignPoints
            .Where(c => c.Description.Equals("Order value to point") && c.StartDate >= timeStamp &&
                        c.EndDate <= timeStamp)
            .Select(c => c.Rate)
            .FirstOrDefaultAsync();
        if (rate == null)
        {
            throw new InvalidOperationException("No valid rate found for the given timestamp.");
        }
        return rate.Value; //output: 0.05
    }
}