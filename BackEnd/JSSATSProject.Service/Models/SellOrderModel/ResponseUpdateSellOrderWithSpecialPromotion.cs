namespace JSSATSProject.Service.Models.OrderModel;

public class ResponseUpdateSellOrderWithSpecialPromotion
{
    public int Id { get; set; }
    public required string CustomerPhoneNumber { get; set; }
    public required string CustomerName { get; set; }
    public decimal? SpecialDiscountRate { get; set; }

    public string? Description { get; set; }
    public required decimal TotalAmount { get; set; }
    public required Dictionary<string, int> ProductCodesAndQuantity { get; set; }
    public required string Status { get; set; }
}