using JSSATSProject.Repository.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace JSSATSProject.Service.Models.BuyOrderModel;

public class RequestUpdateBuyOrderStatus
{
    [Required]

    public  string NewStatus { get; set; }
}