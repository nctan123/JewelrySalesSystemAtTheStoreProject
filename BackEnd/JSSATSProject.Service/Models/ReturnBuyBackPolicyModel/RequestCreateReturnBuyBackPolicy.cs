using System.ComponentModel.DataAnnotations;

namespace JSSATSProject.Service.Models.ReturnBuyBackPolicyModel;

public class RequestCreateReturnBuyBackPolicy
{
    [Required]
    public string Description { get; set; } = null!;
    [Required]
    public DateTime EffectiveDate { get; set; }
}