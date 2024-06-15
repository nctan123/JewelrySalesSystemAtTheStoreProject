using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Models.PromotionRequestModel
{
    public class CreatePromotionRequest
    {
        public int ManagerId { get; set; }

        public string Description { get; set; }

        public decimal DiscountRate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime CreatedAt { get; set; }
        public List<int> CategoriIds { get; set; } = new List<int>();

    }
}
