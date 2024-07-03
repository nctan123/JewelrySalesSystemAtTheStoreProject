using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.MaterialPriceListModel;

namespace JSSATSProject.Service.Service.IService;

public interface IMaterialPriceListService
{
    public Task<ResponseModel> GetAllAsync(DateTime? effectiveDate, int page = 1, int pageSize = 10);
    public Task<ResponseModel> GetByIdAsync(int id);
    public Task<ResponseModel> CreateMaterialPriceListAsync(RequestCreateMaterialPriceList requestMaterialPriceList);

    public Task<ResponseModel> UpdateMaterialPriceListAsync(int materialpricelistId,
        RequestUpdateMaterialPriceList requestMaterialPriceList);
}