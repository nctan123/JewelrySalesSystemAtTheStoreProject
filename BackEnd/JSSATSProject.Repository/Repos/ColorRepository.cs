using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class ColorRepository : GenericRepository<Color>
{
    public ColorRepository(DBContext context) : base(context)
    {
    }
}