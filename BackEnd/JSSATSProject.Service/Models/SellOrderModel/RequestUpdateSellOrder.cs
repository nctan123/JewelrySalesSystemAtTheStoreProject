using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Service.Models.OrderModel;

public class RequestUpdateSellOrder
{
    public SpecialDiscountRequest? SpecialDiscountRequest { get; set; }
    public string? Status { get; set; }

    //public string? Description { get; set; }
}