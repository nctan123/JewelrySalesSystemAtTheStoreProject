using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class PromotionRequestRepository : GenericRepository<PromotionRequest>
{
    public PromotionRequestRepository(DBContext context) : base(context)
    {
    }
}