namespace JSSATSProject.Service.Models.BuyOrderModel;

public class RequestCreateNonCompanyBuyOrder
{
    public required string CustomerPhoneNumber { get; set; }

    public required int StaffId { get; set; }
    
    public string? ProductName { get; set; }

    public string? DiamondGradingCode { get; set; }

    public int? MaterialId { get; set; }

    public decimal? MaterialWeight { get; set; }
    
    public int CategoryTypeId { get; set; }
    
    public decimal BuyPrice { get; set; }
    
    public DateTime CreateDate { get; set; }
    
    public string? Description { get; set; }
}