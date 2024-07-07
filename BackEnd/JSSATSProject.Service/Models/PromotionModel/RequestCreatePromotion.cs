using System.ComponentModel.DataAnnotations;
using JSSATSProject.Repository.CustomValidators;
using JSSATSProject.Repository.CustomValidors;



namespace JSSATSProject.Service.Models.PromotionModel;

[DateRange]
public class RequestCreatePromotion : IDateRange
{
    public string Name { get; set; } = null!;
    
    [Range(0d,1d)]
    public decimal DiscountRate { get; set; }

    public string? Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public List<int> CategoriIds { get; set; } = new();
    

}