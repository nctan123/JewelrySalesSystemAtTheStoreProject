using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class ShapeRepository : GenericRepository<Shape>
{
    public ShapeRepository(DBContext context) : base(context)
    {
    }
}