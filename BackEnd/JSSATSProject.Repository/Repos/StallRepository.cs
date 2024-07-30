using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class StallRepository : GenericRepository<Stall>
{
    public StallRepository(DBContext context) : base(context)
    {
    }
}