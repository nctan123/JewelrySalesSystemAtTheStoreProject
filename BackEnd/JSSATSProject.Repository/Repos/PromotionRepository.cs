using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class PromotionRepository : GenericRepository<Promotion>
{
    public PromotionRepository(DBContext context) : base(context)
    {
    }
}