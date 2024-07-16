using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class StaffRepository : GenericRepository<Staff>
{
    public StaffRepository(DBContext context) : base(context)
    {
    }
}