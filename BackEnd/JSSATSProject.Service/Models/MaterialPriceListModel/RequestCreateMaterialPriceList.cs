using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Models.MaterialPriceListModel
{
    public class RequestCreateMaterialPriceList
    {
        public int MaterialId { get; set; }
        public decimal BuyPrice { get; set; }

        public decimal SellPrice { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
