using JSSATSProject.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JSSATSProject.Service.Models.OrderModel
{
    public class RequestCreateOrder
    {

        public required string CustomerPhoneNumber { get; set; } //

        public int StaffId { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsDraft { get; set; }

        public required string Type { get; set; }

        public string? Description { get; set; }
<<<<<<< HEAD
        public required Dictionary<string, int> ProductCodes { get; set; }
=======

        //public virtual ICollection<OrderDetail> OrderDetails { get; set; }


>>>>>>> ef1d898c610203bb40990ce34f1644abc601b704
    }
}
