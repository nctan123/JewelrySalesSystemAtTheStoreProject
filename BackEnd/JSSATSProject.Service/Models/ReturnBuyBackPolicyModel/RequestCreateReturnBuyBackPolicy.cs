namespace JSSATSProject.Service.Models.ReturnBuyBackPolicyModel;

public class RequestCreateReturnBuyBackPolicy
{
    public string Description { get; set; } = null!;

    public DateTime EffectiveDate { get; set; }
}