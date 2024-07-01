namespace JSSATSProject.Service.Models.SpecialDiscountRequestModel;

public class CreateSpecialDiscountRequest
{
    public string CustomerPhoneNumber { get; set; }
    public int StaffId { get; set; }

    public decimal DiscountRate { get; set; }

    public DateTime CreatedAt { get; set; }
}