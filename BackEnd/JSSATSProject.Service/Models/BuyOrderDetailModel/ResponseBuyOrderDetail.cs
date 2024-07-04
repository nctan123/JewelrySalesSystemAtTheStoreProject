namespace JSSATSProject.Service.Models.BuyOrderDetailModel;

public class ResponseBuyOrderDetail
{
    public int Id { get; set; }

    public int? BuyOrderId { get; set; }

    public string? ProductName { get; set; }
    
    public string? CategoryType { get; set; } 

    // public string? CategoryName { get; set; } //
    public string? MaterialName { get; set; } 

    public decimal? MaterialWeight { get; set; }

    public string? DiamondGradingCode { get; set; }
    public decimal? PurchasePriceRatio { get; set; } 

    public required decimal UnitPrice { get; set; }
}