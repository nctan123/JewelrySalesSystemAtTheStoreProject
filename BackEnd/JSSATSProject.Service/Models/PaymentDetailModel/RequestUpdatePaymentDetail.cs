using JSSATSProject.Repository.CustomValidators;

namespace JSSATSProject.Service.Models.PaymentDetailModel;

public class RequestUpdatePaymentDetail
{
    [StatusValidator("completed", "failed")]
    public string Status { get; set; }
}