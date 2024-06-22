using JSSATSProject.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace JSSATSProject.Repository.Repos;

public class PaymentDetailRepository : GenericRepository<PaymentDetail>
{
    public PaymentDetailRepository(DBContext context) : base(context)
    {
    }


    public async Task<PaymentDetail?> UpdateEntityPaymentDetailAsync(PaymentDetail paymentDetail, string status)
    {
        // Find the existing entity in the database
        var existingPaymentDetail = await context.PaymentDetails
            .FirstOrDefaultAsync(pd => pd.Id == paymentDetail.Id);

        if (existingPaymentDetail == null)
        {
            return null; 
        }

       
        existingPaymentDetail.Status = status;
         context.SaveChanges();

        return existingPaymentDetail;
    }
}