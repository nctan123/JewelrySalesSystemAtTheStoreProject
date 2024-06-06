using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class PaymentRepository : GenericRepository<Payment>
{
    public PaymentRepository(DBContext context) : base(context)
    {
    }
}