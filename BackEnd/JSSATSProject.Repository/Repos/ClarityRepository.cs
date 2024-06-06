using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class ClarityRepository : GenericRepository<Clarity>
{
    public ClarityRepository(DBContext context) : base(context)
    {
    }
}