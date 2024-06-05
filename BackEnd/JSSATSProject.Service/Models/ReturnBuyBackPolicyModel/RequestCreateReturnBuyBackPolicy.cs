using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Models.ReturnBuyBackPolicyModel
{
    public class RequestCreateReturnBuyBackPolicy
    {
        public string Description { get; set; } = null!;

        public DateTime EffectiveDate { get; set; }

        public string? Status { get; set; }
    }
}
