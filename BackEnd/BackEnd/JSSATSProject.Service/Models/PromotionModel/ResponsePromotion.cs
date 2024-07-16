using JSSATSProject.Repository.CustomValidators;
using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Service.Models.PromotionModel;

public class ResponsePromotion
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public decimal DiscountRate { get; set; }

    public string? Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    [StatusValidator("active", "inactive")]
    public string? Status { get; set; }

    public virtual ICollection<ProductCategory> Categories { get; set; } = new List<ProductCategory>();
}