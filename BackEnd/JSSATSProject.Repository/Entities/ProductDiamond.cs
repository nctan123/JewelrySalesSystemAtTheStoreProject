using System.Text.Json.Serialization;

namespace JSSATSProject.Repository.Entities;

public class ProductDiamond
{
    public int ProductId { get; set; }

    public int DiamondId { get; set; }

    [JsonIgnore] 
    public virtual Product Product { get; set; }

    public virtual Diamond Diamond { get; set; }
}