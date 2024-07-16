using JSSATSProject.Repository.CustomValidators;

namespace JSSATSProject.Service.Models.PaymentModel;

public class RequestUpdatePayment
{
    [StatusValidator("completed", "failed")]
    public string Status { get; set; }
}