using System.ComponentModel.DataAnnotations;

namespace JSSATSProject.Service.Models.GuaranteeModel;

public class RequestCreateGuarantee
{
    public int ProductId { get; set; }
    [Required]
    public string Description { get; set; } = null!;
    [Required]
    public DateTime EffectiveDate { get; set; }
    [Required]
    public DateTime ExpiredDate { get; set; }
}