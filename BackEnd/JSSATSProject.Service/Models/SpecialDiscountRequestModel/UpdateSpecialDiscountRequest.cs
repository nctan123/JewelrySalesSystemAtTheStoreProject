namespace JSSATSProject.Service.Models.SpecialDiscountRequestModel;

public class UpdateSpecialDiscountRequest
{
    public decimal DiscountRate { get; set; }
    public required string Status { get; set; }
    public int? ApprovedBy { get; set; }
}