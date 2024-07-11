using JSSATSProject.Repository.CustomValidators;

namespace JSSATSProject.Service.Models.SpecialDiscountRequestModel;

public class UpdateSpecialDiscountRequest
{
    public decimal DiscountRate { get; set; }

    [StatusValidator("approved", "rejected", "used")]
    public required string Status { get; set; }
    public int? ApprovedBy { get; set; }
}