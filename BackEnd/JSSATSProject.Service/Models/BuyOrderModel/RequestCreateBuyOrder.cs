using System.ComponentModel.DataAnnotations;
using JSSATSProject.Repository.CustomValidators;

namespace JSSATSProject.Service.Models.BuyOrderModel;

public class RequestCreateBuyOrder
{
    [Phone(ErrorMessage = "{0} must be a valid phone number.")]
    public required string CustomerPhoneNumber { get; set; }

    public required int StaffId { get; set; }
    [DateRangeValidator(startDate:"01/01/2000")]
    public DateTime CreateDate { get; set; }

    public string? Description { get; set; }

    public required Dictionary<string, int> ProductCodesAndQuantity { get; set; }

    public required Dictionary<string, decimal> ProductCodesAndEstimatePrices { get; set; }
}