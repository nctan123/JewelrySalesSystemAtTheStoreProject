namespace JSSATSProject.Service.Models.GuaranteeModel;

public class RequestCreateGuarantee
{
    public int ProductId { get; set; }
    public string Description { get; set; } = null!;

    public DateTime EffectiveDate { get; set; }

    public DateTime ExpiredDate { get; set; }
}