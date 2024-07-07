using JSSATSProject.Repository.CustomValidators;

namespace JSSATSProject.Service.Models.BuyOrderModel;

public class RequestUpdateBuyOrderStatus
{
    public required string NewStatus { get; set; }
}