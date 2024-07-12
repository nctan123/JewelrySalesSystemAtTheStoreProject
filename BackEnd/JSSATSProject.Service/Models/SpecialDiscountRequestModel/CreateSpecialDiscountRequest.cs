using System.ComponentModel.DataAnnotations;

namespace JSSATSProject.Service.Models.SpecialDiscountRequestModel;

public class CreateSpecialDiscountRequest
{
    [Required]
    [Phone]
    public string CustomerPhoneNumber { get; set; }
    public int StaffId { get; set; }
    [Range(0d, 1d)]
    public decimal DiscountRate { get; set; }

    public DateTime CreatedAt { get; set; }
}