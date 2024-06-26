using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Models.PurchasePriceRatioModel
{
    public class RequestCreatePurchasePriceRatio
    {
        public int CategoryTypeId { get; set; }

        public string Type { get; set; }

        public decimal Percentage { get; set; }

        public int ReturnbuybackpolicyId { get; set; }
    }
}
