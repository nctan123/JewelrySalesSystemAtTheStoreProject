using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class CustomerRepository : GenericRepository<Customer>
{
    public CustomerRepository(DBContext context) : base(context)
    {
    }
}