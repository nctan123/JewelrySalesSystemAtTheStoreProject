using JSSATSProject.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace JSSATSProject.Repository.Repos;

public class ReturnBuyBackPolicyRepository : GenericRepository<ReturnBuyBackPolicy>
{
    public ReturnBuyBackPolicyRepository(DBContext context) : base(context)
    {
    }

    public async Task UpdateStatusesAsync()
    {
        var returnBuyBackPolicies = await context.ReturnBuyBackPolicies
            .OrderByDescending(r => r.EffectiveDate)
            .Skip(1)
            .Where(r => r.EffectiveDate < DateTime.Now && r.Status != "inactive")
            .ToListAsync();

        foreach (var policy in returnBuyBackPolicies)
        {
            policy.Status = "inactive";
        }
        await context.SaveChangesAsync();
    }
}