using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class CutRepository : GenericRepository<Cut>
{
    public CutRepository(DBContext context) : base(context)
    {
    }
}