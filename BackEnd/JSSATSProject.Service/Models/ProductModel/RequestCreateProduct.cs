using Microsoft.AspNetCore.Http;

namespace JSSATSProject.Service.Models.ProductModel;

public class RequestCreateProduct
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = null!;

    public decimal? MaterialCost { get; set; }

    public decimal? ProductionCost { get; set; }

    public decimal? GemCost { get; set; }

    public decimal PriceRate { get; set; }

    public IFormFile ImgFile { get; set; }

}