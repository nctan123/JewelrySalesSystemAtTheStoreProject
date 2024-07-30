using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class OriginRepository : GenericRepository<Origin>
{
    public OriginRepository(DBContext context) : base(context)
    {
    }
}