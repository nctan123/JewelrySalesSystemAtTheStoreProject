using JSSATSProject.Service.Models.DiamondModel;
using JSSATSProject.Service.Models.Material;

namespace JSSATSProject.Service.Models.ProductDiamondModel;

public class ResponseGetAllProductDiamondModel
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public string Img { get; set; }
    public string Status { get; set; }

}