namespace JSSATSProject.Service.Models.BuyOrderModel;

public class RequestCreateBuyOrder
{
    public required string CustomerPhoneNumber { get; set; }

    public required int StaffId { get; set; }
    public int? OrderCode { get; set; }
    public int? GuaranteeCode { get; set; }
    public DateTime CreateDate { get; set; }
    public string? Description { get; set; }
    public required Dictionary<string, int> ProductCodesAndQuantity { get; set; }
    public required Dictionary<string, decimal> ProductCodesAndEstimatePrices { get; set; }
}