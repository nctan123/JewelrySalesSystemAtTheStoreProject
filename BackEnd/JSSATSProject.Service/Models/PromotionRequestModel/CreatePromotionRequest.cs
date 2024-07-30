using JSSATSProject.Repository.CustomValidators;
using JSSATSProject.Repository.CustomValidors;

namespace JSSATSProject.Service.Models.PromotionRequestModel;
[DateRange]
public class CreatePromotionRequest : IDateRange
{
    public int ManagerId { get; set; }

    public string Description { get; set; }

    public decimal DiscountRate { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime CreatedAt { get; set; }
    public List<int> CategoriIds { get; set; } = new();
}