using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class PaymentMethodRepository : GenericRepository<PaymentMethod>
{
    public PaymentMethodRepository(DBContext context) : base(context)
    {
    }
}