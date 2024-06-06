using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class PointRepository : GenericRepository<Point>
{
    public PointRepository(DBContext context) : base(context)
    {
    }
}