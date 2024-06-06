using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class RoleRepository : GenericRepository<Role>
{
    public RoleRepository(DBContext context) : base(context)
    {
    }
}