using JSSATSProject.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Models.OrderDetail
{
    public class RequestCreateOrderDetail
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public string Size { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public decimal SizePrice { get; set; }

        public string Status { get; set; }

        public virtual Product Product { get; set; }
    }
}
