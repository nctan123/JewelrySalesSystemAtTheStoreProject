using System.ComponentModel.DataAnnotations;
using JSSATSProject.Repository.CustomValidators;

namespace JSSATSProject.Service.Models.PromotionModel;

public class RequestCreatePromotion
{
    public string Name { get; set; } = null!;
    
    [Range(0d,1d)]
    public decimal DiscountRate { get; set; }

    public string? Description { get; set; }

    [DateRangeValidator(startDate: "01/01/2024", endDate:"01/01/2030")]
    public DateTime StartDate { get; set; }

    [DateRangeValidator(startDate: "01/01/2024", endDate:"01/01/2030")]
    public DateTime EndDate { get; set; }

    [StatusValidator("active", "inactive")]
    public string? Status { get; set; }

    public List<int> CategoriIds { get; set; } = new();
    

}