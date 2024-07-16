using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class MaterialRepository : GenericRepository<Material>
{
    public MaterialRepository(DBContext context) : base(context)
    {
    }
}