using System.Runtime.InteropServices.JavaScript;
using JSSATSProject.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace JSSATSProject.Repository.Repos;

public class GuaranteePolicyRepository : GenericRepository<GuaranteePolicy>
{
    public GuaranteePolicyRepository(DBContext context) : base(context)
    {
    }

    public async Task<GuaranteePolicy?> GetEntityAsync(int categoryId, DateTime timeStamp)
    {
        return await context.GuaranteePolicies
            .FirstOrDefaultAsync(gp => gp.ProductCategoryId == categoryId
                                       && gp.StartDate <= timeStamp
                                       && gp.EndDate >= timeStamp
            );
    }
}