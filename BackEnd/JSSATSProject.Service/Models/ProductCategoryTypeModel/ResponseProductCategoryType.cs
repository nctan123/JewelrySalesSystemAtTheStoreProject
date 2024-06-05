using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Models.ProductCategoryTypeModel
{
    public class ResponseProductCategoryType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Status { get; set; }
    }
}
