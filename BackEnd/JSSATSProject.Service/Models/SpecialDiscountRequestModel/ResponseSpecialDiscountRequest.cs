using JSSATSProject.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Models.SpecialDiscountRequestModel
{
    public class ResponseSpecialDiscountRequest
    {
        public int RequestId { get; set; }
        public int CustomerId { get; set; }

        public int StaffId { get; set; }

        public decimal DiscountRate { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public int? ApprovedBy { get; set; }

        public virtual Staff ApprovedByNavigation { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
