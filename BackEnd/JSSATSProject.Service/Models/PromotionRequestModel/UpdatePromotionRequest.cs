using JSSATSProject.Repository.CustomValidators;

namespace JSSATSProject.Service.Models.PromotionRequestModel;

public class UpdatePromotionRequest
{
    public string Description { get; set; }

    public decimal DiscountRate { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    [StatusValidator("approved", "rejected")]
    public string Status { get; set; }
    public int? ApprovedBy { get; set; }
}