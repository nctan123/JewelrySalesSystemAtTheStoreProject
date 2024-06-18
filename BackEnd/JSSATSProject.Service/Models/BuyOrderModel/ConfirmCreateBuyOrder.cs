namespace JSSATSProject.Service.Models.OrderModel;

public class ConfirmCreateBuyOrder
{
    public int CustomerPhoneNumber { get; set; }

    public int StaffId { get; set; }

    public DateTime CreateDate { get; set; }

    public string? Description { get; set; }

    public required Dictionary<string, int> ProductCodesAndQuantity { get; set; }

    public required Dictionary<string, string> ProductCodesAndPromotionIds { get; set; }

    // public required string? SpecialDiscountRequestId { get; set; }

    //for handing special discount
    public bool IsSpecialDiscountRequested { get; set; }
    public decimal? SpecialDiscountRate { get; set; }
        
    public string? DiscountRejectedReason { get; set; }
}