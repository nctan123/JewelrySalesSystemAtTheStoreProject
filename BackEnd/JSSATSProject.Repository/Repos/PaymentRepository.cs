using JSSATSProject.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace JSSATSProject.Repository.Repos;

public class PaymentRepository : GenericRepository<Payment>
{
    public PaymentRepository(DBContext context) : base(context)
    {
    }

    
}