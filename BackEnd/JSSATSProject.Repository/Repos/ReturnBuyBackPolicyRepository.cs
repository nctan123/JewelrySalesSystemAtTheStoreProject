using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class ReturnBuyBackPolicyRepository : GenericRepository<ReturnBuyBackPolicy>
{
    public ReturnBuyBackPolicyRepository(DBContext context) : base(context)
    {
    }
}