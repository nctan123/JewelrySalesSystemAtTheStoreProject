using JSSATSProject.Repository.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace JSSATSProject.Service.Models.PaymentDetailModel;

public class RequestCreatePaymentDetail
{
    public int PaymentId { get; set; }

    public int PaymentMethodId { get; set; }
    
    [Range(0, (double)decimal.MaxValue)]
    public decimal Amount { get; set; }

    public string ExternalTransactionCode { get; set; }

    [StatusValidator("completed", "failed")]
    public string Status { get; set; }
}