namespace JSSATSProject.Service.Models.SpecialDiscountRequestModel;

public class ResponseSpecialDiscountRequest
{
    public int RequestId { get; set; }
    public string CustomerPhone { get; set; }
    public string CustomerName { get; set; }
    public string StaffName { get; set; }

    public decimal DiscountRate { get; set; }

    public DateTime CreatedAt { get; set; }

    public string ApprovedName { get; set; }

    public string Status { get; set; }

    public int SellOrderId { get; set; }

    public string  SellOrderCode { get; set; }
}