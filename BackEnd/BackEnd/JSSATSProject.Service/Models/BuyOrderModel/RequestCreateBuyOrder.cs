using JSSATSProject.Repository.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace JSSATSProject.Service.Models.BuyOrderModel;

public class RequestCreateBuyOrder
{
    [Required]
    [VietnamesePhone(ErrorMessage = "{0} must be a valid Vietnamese phone number.")]
    public required string CustomerPhoneNumber { get; set; }
    [Required]
    public required int StaffId { get; set; }

    public DateTime CreateDate { get; set; }

    public string? Description { get; set; }
    [Required]
    public required Dictionary<string, int> ProductCodesAndQuantity { get; set; }
    [Required]
    public required Dictionary<string, decimal> ProductCodesAndEstimatePrices { get; set; }
}