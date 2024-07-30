using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Models.CustomerModel
{
    public class ResponseCustomerSummary
    {
        public string CustomerPhone { get; set; }
        //BuyOrder
        public int BuyOrderCompletedCount { get; set; }
        public int BuyOrderOtherCount { get; set; }
        public decimal BuyOrderCompletedSum { get; set; }
        public decimal BuyOrderOtherSum { get; set; }
        //SellOrder
        public int SellOrderCompletedCount { get; set; }
        public int SellOrderOtherCount { get; set; }
        public decimal SellOrderCompletedSum { get; set; }
        public decimal SellOrderOtherSum { get; set; }

        // Payment
        public int PaymentCompletedCount { get; set; }
        public int PaymentPendingCount { get; set; }
        public decimal PaymentPendingSum { get; set; }
         public decimal PaymentCompletedSum { get; set; }

    }
}
