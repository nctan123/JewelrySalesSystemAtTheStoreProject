using JSSATSProject.Repository.Enums;

namespace JSSATSProject.Service.Models.OrderModel;

public class RequestCreateSellOrder
{
    public int? Id { get; set; }
    public required string CustomerPhoneNumber { get; set; }
    public required int StaffId { get; set; }
    public DateTime CreateDate { get; set; }
    public int DiscountPoint { get; set; }
    public string? Description { get; set; }
    public required Dictionary<string, int> ProductCodesAndQuantity { get; set; }
    public required Dictionary<string, int?>? ProductCodesAndPromotionIds { get; set; }
    public required int? SpecialDiscountRequestId { get; set; }

    //for handing special discount
    public bool IsSpecialDiscountRequested { get; set; }
    public decimal? SpecialDiscountRate { get; set; }

    public string? DiscountRejectedReason { get; set; }
    public SpecialDiscountRequestStatus? SpecialDiscountRequestStatus { get; set; }
}