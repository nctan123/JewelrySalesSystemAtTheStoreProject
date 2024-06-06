using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Repository.Repos;

public class PaymentDetailRepository : GenericRepository<PaymentDetail>
{
    public PaymentDetailRepository(DBContext context) : base(context)
    {
    }
}