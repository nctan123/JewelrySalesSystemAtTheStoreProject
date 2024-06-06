using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class SymmetryRepository : GenericRepository<Symmetry>
{
    public SymmetryRepository(DBContext context) : base(context)
    {
    }
}