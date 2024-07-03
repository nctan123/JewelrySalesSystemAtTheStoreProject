using System.Text.Json.Serialization;
using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Service.Models.SellOrderDetailsModel;

public class ResponseSellOrderDetails
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public required string ProductName { get; set; }
    public required string ProductCode { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public required string Status { get; set; }

    [JsonIgnore]
    public virtual Product Product { get; set; }

    public int? PromotionId { get; set; }
}