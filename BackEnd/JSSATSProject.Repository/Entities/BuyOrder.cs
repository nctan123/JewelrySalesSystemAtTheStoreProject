using System.Text.Json.Serialization;
using JSSATSProject.Repository.CustomValidators;

namespace JSSATSProject.Repository.Entities;

public partial class BuyOrder
{
    public int Id { get; set; }
    public string Code { get; set; }
    
    [JsonIgnore]
    public int CustomerId { get; set; }
    
    [JsonIgnore]
    public int StaffId { get; set; }

    public decimal TotalAmount { get; set; }
    
    public DateTime CreateDate { get; set; }

    public string Description { get; set; }

    [StatusValidator("processing", "cancelled", "completed")]
    public string Status { get; set; }
    public virtual ICollection<BuyOrderDetail> BuyOrderDetails { get; set; } = new List<BuyOrderDetail>();
    
    [JsonIgnore]
    public virtual Customer Customer { get; set; }
    
    [JsonIgnore]
    public virtual Staff Staff { get; set; }

}