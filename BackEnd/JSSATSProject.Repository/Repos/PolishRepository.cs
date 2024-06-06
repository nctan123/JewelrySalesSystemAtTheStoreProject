using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class PolishRepository : GenericRepository<Polish>
{
    public PolishRepository(DBContext context) : base(context)
    {
    }
}