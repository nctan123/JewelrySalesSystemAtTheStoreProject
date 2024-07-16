namespace JSSATSProject.Service.Models.PaymentMethodModel;

public class RequestCreatePaymentMethod
{
    public string Name { get; set; } = null!;
    public string? Status { get; set; }
}