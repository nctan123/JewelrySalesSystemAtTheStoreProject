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
}