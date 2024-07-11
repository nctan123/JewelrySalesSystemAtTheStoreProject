using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.DiamondPriceListModel;

namespace JSSATSProject.Service.Service.IService;

public interface IDiamondPriceListService
{
    public Task<ResponseModel> GetAllAsync();
    public Task<ResponseModel> GetByIdAsync(int id);
    public Task<ResponseModel> CreateDiamondPriceListAsync(RequestCreateDiamondPriceList requestDiamondPriceList);

    public Task<ResponseModel> UpdateDiamondPriceListAsync(int diamondpricelistId,
        RequestUpdateDiamondPriceList requestDiamondPriceList);

    public Task<decimal> FindPriceBy4CAndOriginAndFactors(int cutId, int clarityId, int colorId, int caratId,
        int originId, decimal totalFactors, DateTime maxEffectiveDay);
    
}