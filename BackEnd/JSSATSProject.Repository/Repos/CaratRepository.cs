using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class CaratRepository : GenericRepository<Carat>
{
    public CaratRepository(DBContext context) : base(context)
    {
    }
}