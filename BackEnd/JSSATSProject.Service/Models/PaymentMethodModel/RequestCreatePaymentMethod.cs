using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Models.PaymentMethodModel
{
    
    public class RequestCreatePaymentMethod
    {
        public string Name { get; set; } = null!;
        public string? Status { get; set; }

    }
}
