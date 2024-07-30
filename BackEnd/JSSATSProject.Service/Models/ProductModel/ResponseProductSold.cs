namespace JSSATSProject.Service.Models.ProductModel;

public class ResponseProductSold
{
    public string SellOrderCode { get; set; }

    public string ProductName { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public decimal? PromotionRate { get; set; }
    public string? GuaranteeCode { get; set; }
}