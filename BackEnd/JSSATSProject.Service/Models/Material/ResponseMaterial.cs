using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Service.Models.Material;

public class ResponseMaterial
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<MaterialPriceList> MaterialPriceLists { get; set; } = new List<MaterialPriceList>();

    public virtual ICollection<ProductMaterial> ProductMaterials { get; set; } = new List<ProductMaterial>();
}