using System.Text.Json.Serialization;
using JSSATSProject.Repository.CustomValidators;

namespace JSSATSProject.Repository.Entities;

public partial class BuyOrder
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int StaffId { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime CreateDate { get; set; }

    public string Description { get; set; }

    public string Status { get; set; }

    public string Code { get; set; }

    public virtual ICollection<BuyOrderDetail> BuyOrderDetails { get; set; } = new List<BuyOrderDetail>();

    public virtual Customer Customer { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    [JsonIgnore]
    public virtual Staff Staff { get; set; }
}