namespace JSSATSProject.Service.Models.PaymentModel;

public class RequestCreatePayment
{
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public DateTime CreateDate { get; set; }
    public decimal Amount { get; set; }
}