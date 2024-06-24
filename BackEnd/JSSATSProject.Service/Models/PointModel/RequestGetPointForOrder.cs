using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Models.PointModel
{
    public class RequestGetPointForOrder
    {
        public required string CustomerPhoneNumber { get; set; }
        public required decimal TotalOrderPrice { get; set; }
    }
}
