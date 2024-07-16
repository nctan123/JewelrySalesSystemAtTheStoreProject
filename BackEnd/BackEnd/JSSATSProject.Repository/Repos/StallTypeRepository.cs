using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class StallTypeRepository : GenericRepository<StallType>
{
    public StallTypeRepository(DBContext context) : base(context)
    {
    }
}