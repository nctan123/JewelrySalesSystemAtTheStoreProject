using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Models.PurchasePriceRatioModel
{
    public class ResponsePurchasePriceRatio
    {
        public int ReturnbuybackpolicyId { get; set; }
        public int CategoryTypeName { get; set; }

        public string Type { get; set; }

        public decimal Percentage { get; set; }

    }
}
