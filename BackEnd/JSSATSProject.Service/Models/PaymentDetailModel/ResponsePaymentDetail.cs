﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Models.PaymentDetailModel
{
    public class ResponsePaymentDetail
    {
        public int Id { get; set; }
        public int PaymentId { get; set; }

        public int PaymentMethodId { get; set; }

        public decimal Amount { get; set; }

        public string ExternalTransactionCode { get; set; }

        public string Status { get; set; }
    }
}
