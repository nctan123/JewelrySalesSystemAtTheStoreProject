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
        public int Id { get; set; }

        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public required string ProductCode { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public required string Status { get; set; }

        public int? PromotionId { get; set; }
    }
}