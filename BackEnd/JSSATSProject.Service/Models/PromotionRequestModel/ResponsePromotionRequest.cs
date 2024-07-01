using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Service.Models.PromotionRequestModel;

public class ResponsePromotionRequest
{
    public int RequestId { get; set; }
    public string ManagerName { get; set; }

    public string Description { get; set; }

    public decimal DiscountRate { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public string ApprovedName { get; set; }
    public virtual ICollection<ProductCategory> Categories { get; set; } = new List<ProductCategory>();
}