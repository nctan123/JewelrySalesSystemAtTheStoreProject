using System.ComponentModel.DataAnnotations;

namespace JSSATSProject.Service.Models.PaymentModel;

public class RequestCreatePayment
{
    public int? SellOrderId { get; set; }
    public int? BuyOrderId { get; set; }
    public int CustomerId { get; set; }
    public DateTime CreateDate { get; set; }
    
    [Range(0, (double)decimal.MaxValue)]
    public decimal Amount { get; set; }
}