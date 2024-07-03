namespace JSSATSProject.Service.Models.PromotionModel;

public class RequestCreatePromotion
{
    public string Name { get; set; } = null!;

    public decimal DiscountRate { get; set; }

    public string? Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? Status { get; set; }

    public List<int> CategoriIds { get; set; } = new();
}