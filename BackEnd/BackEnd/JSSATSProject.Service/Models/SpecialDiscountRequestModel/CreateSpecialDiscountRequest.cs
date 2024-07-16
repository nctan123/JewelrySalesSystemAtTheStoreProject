using JSSATSProject.Repository.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace JSSATSProject.Service.Models.SpecialDiscountRequestModel;

public class CreateSpecialDiscountRequest
{
    [Required]
    [VietnamesePhone(ErrorMessage = "{0} must be a valid Vietnamese phone number.")]
    public string CustomerPhoneNumber { get; set; }
    public int StaffId { get; set; }
    [Range(0d, 1d)]
    public decimal DiscountRate { get; set; }

    public DateTime CreatedAt { get; set; }
}