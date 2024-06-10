using JSSATSProject.Service.Models.DiamondModel;
using JSSATSProject.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JSSATSProject.Service.Models.DiamondPriceListModel;

namespace JSSATSProject.Service.Service.IService
{
    public interface IDiamondPriceListService
    {
        public Task<ResponseModel> GetAllAsync();
        public Task<ResponseModel> GetByIdAsync(int id);
        public Task<ResponseModel> CreateDiamondPriceListAsync(RequestCreateDiamondPriceList requestDiamondPriceList);
        public Task<ResponseModel> UpdateDiamondPriceListAsync(int diamondpricelistId, RequestUpdateDiamondPriceList requestDiamondPriceList);
    }
}