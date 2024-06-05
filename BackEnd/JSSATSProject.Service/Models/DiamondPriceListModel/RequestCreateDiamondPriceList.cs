using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Service.Models.DiamondPriceListModel
{
    public class RequestCreateDiamondPriceList
    {

        public int ColorId { get; set; }

        public int CutId { get; set; }

        public int OriginId { get; set; }

        public int ClarityId { get; set; }

        public int CaratId { get; set; }

        public decimal Price { get; set; }

        public DateTime EffectiveDate { get; set; }

   
    }
}
