using System.ComponentModel.DataAnnotations;

namespace JSSATSProject.Service.Models.BuyOrderModel;

public class RequestCreateBuyOrder
{
    [Required]
    [Phone]
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