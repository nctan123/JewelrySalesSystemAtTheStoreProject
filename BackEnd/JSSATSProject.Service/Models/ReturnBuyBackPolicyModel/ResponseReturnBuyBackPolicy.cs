namespace JSSATSProject.Service.Models.ReturnBuyBackPolicyModel;

public class ResponseReturnBuyBackPolicy
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public DateTime EffectiveDate { get; set; }

    public string? Status { get; set; }
}