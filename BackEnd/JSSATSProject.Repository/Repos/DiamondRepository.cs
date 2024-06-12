using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class DiamondRepository : GenericRepository<Diamond>
{
    public DiamondRepository(DBContext context) : base(context)
    {
    }


}