using System.ComponentModel.DataAnnotations;

namespace JSSATSProject.Service.Models.MaterialPriceListModel;

public class RequestCreateMaterialPriceList
{
    public int MaterialId { get; set; }
    [Range(0.01, 99999999.99, ErrorMessage = "BuyPrice must be greater than 0.")]
    public decimal BuyPrice { get; set; }

    [Range(0.01, 99999999.99, ErrorMessage = "SellPrice must be greater than 0.")]
    public decimal SellPrice { get; set; }

    public DateTime EffectiveDate { get; set; }
}