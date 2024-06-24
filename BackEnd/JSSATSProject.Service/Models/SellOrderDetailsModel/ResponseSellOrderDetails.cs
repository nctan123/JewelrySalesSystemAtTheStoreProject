using JSSATSProject.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Models.SellOrderDetailsModel
{
    public class ResponseSellOrderDetails
    {
       
        public int ProductId { get; set; }

        public string Name { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public int? PromotionId { get; set; }

        public int OrderId { get; set; }
    }
}
