namespace JSSATSProject.Service.Models.ProductModel;

//dto cua product khi an nut check trong order details
public class ResponseProductForCheckOrder
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public decimal? PriceInOrder { get; set; }

    //quantity or weight if product is wholesale gold
    public decimal? Quantity { get; set; }

    public decimal? EstimateBuyPrice { get; set; }

    public string? ReasonForEstimateBuyPrice { get; set; }
}