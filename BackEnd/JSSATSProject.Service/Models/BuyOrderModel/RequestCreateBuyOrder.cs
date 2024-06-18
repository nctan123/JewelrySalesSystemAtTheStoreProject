namespace JSSATSProject.Service.Models.OrderModel;

public class RequestCreateBuyOrder
{
    public int CustomerId { get; set; }

    public int StaffId { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime CreateDate { get; set; }

    public string Description { get; set; }

}