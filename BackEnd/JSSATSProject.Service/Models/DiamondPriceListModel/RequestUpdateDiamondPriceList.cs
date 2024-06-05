using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Models.DiamondPriceListModel
{
    public class RequestUpdateDiamondPriceList
    {
        public decimal Price { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
