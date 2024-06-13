using JSSATSProject.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Models.SpecialDiscountRequestModel
{
    public class CreateSpecialDiscountRequest
    {
        public int CustomerId { get; set; }

        public int StaffId { get; set; }

        public decimal DiscountRate { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
