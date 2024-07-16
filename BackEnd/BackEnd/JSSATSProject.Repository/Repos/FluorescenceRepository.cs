using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class FluorescenceRepository : GenericRepository<Fluorescence>
{
    public FluorescenceRepository(DBContext context) : base(context)
    {
    }
}