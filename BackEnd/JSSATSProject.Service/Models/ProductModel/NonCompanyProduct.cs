namespace JSSATSProject.Service.Models.ProductModel;

public class NonCompanyProduct
{
    public string? ProductName { get; set; }

    public string? DiamondGradingCode { get; set; }

    public int? MaterialId { get; set; }

    public decimal? MaterialWeight { get; set; }

    public int CategoryTypeId { get; set; }

    public decimal BuyPrice { get; set; }
    
    public int? Quantity { get; set; } = 1;

}