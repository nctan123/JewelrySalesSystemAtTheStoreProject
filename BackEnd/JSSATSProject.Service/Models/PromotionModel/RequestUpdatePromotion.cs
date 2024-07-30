using JSSATSProject.Repository.CustomValidators;
using JSSATSProject.Repository.CustomValidors;

namespace JSSATSProject.Service.Models.PromotionModel;
[DateRange]
public class RequestUpdatePromotion : IDateRange
{
    public string Name { get; set; } = null!;

    public decimal DiscountRate { get; set; }

    public string? Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    [StatusValidator("active", "inactive")]
    public string? Status { get; set; }
}