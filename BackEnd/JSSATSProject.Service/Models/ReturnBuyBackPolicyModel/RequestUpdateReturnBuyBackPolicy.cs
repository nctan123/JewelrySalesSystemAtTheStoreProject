namespace JSSATSProject.Service.Models.ReturnBuyBackPolicyModel;

public class RequestUpdateReturnBuyBackPolicy
{
    public string Description { get; set; } = null!;

    public DateTime EffectiveDate { get; set; }

    public string? Status { get; set; }
}