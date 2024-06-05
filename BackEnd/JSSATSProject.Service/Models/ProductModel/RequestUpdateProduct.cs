using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Models.ProductModel
{
    public class RequestUpdateProduct
    {
        public decimal PriceRate { get; set; }

        public string? Status { get; set; }
    }
}
