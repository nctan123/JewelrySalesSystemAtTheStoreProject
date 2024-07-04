namespace JSSATSProject.Service.Models.BuyOrderModel;

public class RequestCreateNonCompanyBuyOrder
{
    public required string CustomerPhoneNumber { get; set; }

    public required int StaffId { get; set; }
    
    public string? ProductName { get; set; }

    public int? Quantity { get; set; } = 1;

    public string? DiamondGradingCode { get; set; } = null;

    public int? MaterialId { get; set; } = null;

    public decimal? MaterialWeight { get; set; } = null;
    
    public int CategoryTypeId { get; set; } 
    
    public decimal BuyPrice { get; set; }
    
    public DateTime CreateDate { get; set; }
    
    public string? Description { get; set; }
}