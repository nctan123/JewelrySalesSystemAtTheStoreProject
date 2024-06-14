using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JSSATSProject.Repository.Entities {
    public partial class ProductDiamond
{
    public int ProductId { get; set; }
   
    public int DiamondId { get; set; }

    [JsonIgnore]
    public virtual Product Product { get; set; }

    public virtual Diamond Diamond { get; set; }

}
}
