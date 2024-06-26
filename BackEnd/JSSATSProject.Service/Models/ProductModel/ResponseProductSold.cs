using JSSATSProject.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Models.ProductModel
{
    public class ResponseProductSold
    {
        public string SellOrderCode { get; set; }

        public string ProductName { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public decimal? PromotionRate { get; set; }
        public string? GuaranteeCode { get; set; }


    }
}
