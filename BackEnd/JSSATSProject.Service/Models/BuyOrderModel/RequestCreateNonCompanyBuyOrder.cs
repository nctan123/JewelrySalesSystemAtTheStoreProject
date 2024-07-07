using JSSATSProject.Service.Models.ProductModel;

namespace JSSATSProject.Service.Models.BuyOrderModel;

public class RequestCreateNonCompanyBuyOrder
{
    public required string CustomerPhoneNumber { get; set; }

    public required int StaffId { get; set; }
    public DateTime CreateDate { get; set; }
    public required List<NonCompanyProduct> Products { get; set; } = [];
    public string? Description { get; set; }
}