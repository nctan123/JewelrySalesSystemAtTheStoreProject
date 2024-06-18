using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Models.PromotionRequestModel
{
    public class UpdatePromotionRequest
    {
        public string Description { get; set; }

        public decimal DiscountRate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public int? ApprovedBy { get; set; }

    }
}
