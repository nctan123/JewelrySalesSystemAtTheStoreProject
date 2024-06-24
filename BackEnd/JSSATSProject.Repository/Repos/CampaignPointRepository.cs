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
}