namespace JSSATSProject.Service.Models.GuaranteeModel;

public class RequestUpdateGuarantee
{
    public string Description { get; set; } = null!;

    public DateTime EffectiveDate { get; set; }

    public DateTime ExpiredDate { get; set; }
}