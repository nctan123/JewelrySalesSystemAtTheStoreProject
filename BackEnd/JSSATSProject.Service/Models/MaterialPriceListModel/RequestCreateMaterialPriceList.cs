namespace JSSATSProject.Service.Models.MaterialPriceListModel;

public class RequestCreateMaterialPriceList
{
    public int MaterialId { get; set; }
    public decimal BuyPrice { get; set; }

    public decimal SellPrice { get; set; }

    public DateTime EffectiveDate { get; set; }
}