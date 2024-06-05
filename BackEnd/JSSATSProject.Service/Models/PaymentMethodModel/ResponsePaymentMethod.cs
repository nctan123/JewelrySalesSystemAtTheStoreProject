using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Models.PaymentMethodModel
{
    public class ResponsePaymentMethod
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Status { get; set; }

        //public virtual ICollection<PaymentDetail> PaymentDetails { get; set; } = new List<PaymentDetail>();
    }
}
