using System.ComponentModel.DataAnnotations;

namespace JSSATSProject.Service.Models.ProductModel;

public class RequestCreateProduct
{
    public int CategoryId { get; set; }
    
    public string Name { get; set; } = null!;
    
    [Range(0, (double)decimal.MaxValue)]
    public decimal? MaterialCost { get; set; }
    
    [Range(0, (double)decimal.MaxValue)]
    public decimal? ProductionCost { get; set; }
    
    [Range(0, (double)decimal.MaxValue)]
    public decimal? GemCost { get; set; }

    public string? Img { get; set; }

    [Range(1, Int32.MaxValue)]
    public decimal PriceRate { get; set; }

    public string? Status { get; set; }
}