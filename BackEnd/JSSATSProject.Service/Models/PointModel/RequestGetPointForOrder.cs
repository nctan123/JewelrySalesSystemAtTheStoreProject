namespace JSSATSProject.Service.Models.PointModel;

public class RequestGetPointForOrder
{
    public required string CustomerPhoneNumber { get; set; }
    public required decimal TotalOrderPrice { get; set; }
}