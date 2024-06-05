using JSSATSProject.Service.Models.GuaranteeModel;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.MaterialPriceListModel;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.IService
{
    public interface IMaterialPriceListService
    {
        public Task<ResponseModel> GetAllAsync();
        public Task<ResponseModel> GetByIdAsync(int id);
        public Task<ResponseModel> CreateMaterialPriceListAsync(RequestCreateMaterialPriceList requestMaterialPriceList);
        public Task<ResponseModel> UpdateMaterialPriceListAsync(int materialpricelistId, RequestUpdateMaterialPriceList requestMaterialPriceList);
    }
}