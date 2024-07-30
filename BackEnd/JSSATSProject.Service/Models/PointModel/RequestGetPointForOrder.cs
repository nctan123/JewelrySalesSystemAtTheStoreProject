using System.ComponentModel.DataAnnotations;

namespace JSSATSProject.Service.Models.PointModel;

public class RequestGetPointForOrder
{
    [Phone]
    public required string CustomerPhoneNumber { get; set; }
    public required decimal TotalOrderPrice { get; set; }
}