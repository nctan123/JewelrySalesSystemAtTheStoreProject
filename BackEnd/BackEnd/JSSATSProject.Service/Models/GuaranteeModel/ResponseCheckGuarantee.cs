using JSSATSProject.Service.Models.ProductModel;

namespace JSSATSProject.Service.Models.GuaranteeModel;

public class ResponseCheckGuarantee
{
    public required string Code { get; set; }
    public DateTime EffectiveDate { get; set; }
    public DateTime ExpireDate { get; set; }
    public required string CustomerPhoneNumber { get; set; }
    public required string CustomerName { get; set; }
    public required ResponseProductForCheckOrder Product { get; set; }
}