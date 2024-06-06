using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class GuaranteeRepository : GenericRepository<Guarantee>
{
    public GuaranteeRepository(DBContext context) : base(context)
    {
    }
}