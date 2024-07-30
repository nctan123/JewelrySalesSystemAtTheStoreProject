namespace JSSATSProject.Service.Models.MaterialPriceListModel;

public class RequestUpdateMaterialPriceList
{
    public decimal BuyPrice { get; set; }

    public decimal SellPrice { get; set; }

    public DateTime EffectiveDate { get; set; }
}