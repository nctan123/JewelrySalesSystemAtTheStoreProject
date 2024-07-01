using JSSATSProject.Service.Models.ProductModel;

namespace JSSATSProject.Service.Models.OrderModel;

//an nut check de xem order details
public class ResponseCheckOrder
{
    public required string code { get; set; }
    public decimal TotalValue { get; set; }
    public DateTime CreateDate { get; set; }
    public required string CustomerPhoneNumber { get; set; }
    public required string CustomerName { get; set; }
    public required List<ResponseProductForCheckOrder> Products { get; set; }
}