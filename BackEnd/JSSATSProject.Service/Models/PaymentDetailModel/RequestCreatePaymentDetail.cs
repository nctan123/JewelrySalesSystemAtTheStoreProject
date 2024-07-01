namespace JSSATSProject.Service.Models.PaymentDetailModel;

public class RequestCreatePaymentDetail
{
    public int PaymentId { get; set; }

    public int PaymentMethodId { get; set; }

    public decimal Amount { get; set; }

    public string ExternalTransactionCode { get; set; }

    public string Status { get; set; }
}