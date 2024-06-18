using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Service.Models.OrderModel
{
    public class RequestUpdateSellOrder
    {
        public SpecialDiscountRequest? SpecialDiscountRequest { get; set; }
        public string? Status { get; set; }
        
        public string? Description { get; set; }
    }
}