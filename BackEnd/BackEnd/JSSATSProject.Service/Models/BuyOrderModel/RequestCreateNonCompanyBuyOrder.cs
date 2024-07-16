using JSSATSProject.Repository.CustomValidators;
using JSSATSProject.Service.Models.ProductModel;
using System.ComponentModel.DataAnnotations;

namespace JSSATSProject.Service.Models.BuyOrderModel;

public class RequestCreateNonCompanyBuyOrder
{
    [Required]
    [VietnamesePhone(ErrorMessage = "{0} must be a valid Vietnamese phone number.")]
    public required string CustomerPhoneNumber { get; set; }

    public required int StaffId { get; set; }

    public DateTime CreateDate { get; set; }

    public required List<NonCompanyProduct> Products { get; set; } = [];
    public string? Description { get; set; }
}