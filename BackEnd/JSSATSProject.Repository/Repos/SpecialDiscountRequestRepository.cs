using JSSATSProject.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JSSATSProject.Repository.Repos
{
    public class SpecialDiscountRequestRepository : GenericRepository<SpecialDiscountRequest>
    {
        public SpecialDiscountRequestRepository(DBContext context) : base(context)
        {
        }

        public async Task<SpecialDiscountRequest?> GetByIdAsync(int id)
        {
            return await context.SpecialDiscountRequests
                    .Where(s => s.RequestId == id)
                    .Include(s => s.SellOrders)
                    .FirstOrDefaultAsync()
                ;
        }

        public async Task<SpecialDiscountRequest?> GetByOrderCodeAsync(string orderCode)
        {
            var sellOrder = await context.SellOrders
                .Include(s => s.SpecialDiscountRequest)
                    .ThenInclude(s => s.ApprovedBy)
                .Where(s => s.Code == orderCode)
                .FirstOrDefaultAsync();

            return sellOrder?.SpecialDiscountRequest;
        }
    }
}