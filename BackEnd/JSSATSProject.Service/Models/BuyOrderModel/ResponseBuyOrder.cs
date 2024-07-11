using System.ComponentModel.DataAnnotations;
using JSSATSProject.Service.Models.BuyOrderDetailModel;

namespace JSSATSProject.Service.Models.BuyOrderModel;

public class ResponseBuyOrder
{
    public int Id { get; set; }

    public string Code { get; set; }

    public int CustomerId { get; set; } 

    public required string CustomerName { get; set; } 

    public required string CustomerPhoneNumber { get; set; } 

    public required int StaffId { get; set; }

    public required string StaffName { get; set; } 

    public DateTime CreateDate { get; set; }

    [Range(0, (double)decimal.MaxValue)]
    public decimal TotalAmount { get; set; }

    public string Description { get; set; }

    public string Status { get; set; }

    public virtual ICollection<ResponseBuyOrderDetail> BuyOrderDetails { get; set; } =
        new List<ResponseBuyOrderDetail>();

    public string PaymentMethod { get; set; }
}