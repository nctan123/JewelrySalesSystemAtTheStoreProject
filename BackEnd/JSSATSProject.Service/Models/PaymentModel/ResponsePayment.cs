namespace JSSATSProject.Service.Models.PaymentModel;

public class ResponsePayment
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public string OrderCode { get; set; }

    public int CustomerId { get; set; }

    public DateTime CreateDate { get; set; }

    public decimal Amount { get; set; }

    public string Status { get; set; }
    public string ExternalTransactionCode { get; set; }

    public string PaymentMethodName { get; set; }
}