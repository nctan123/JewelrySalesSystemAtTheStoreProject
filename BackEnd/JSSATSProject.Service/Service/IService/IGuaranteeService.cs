using JSSATSProject.Service.Models.DiamondModel;
using JSSATSProject.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JSSATSProject.Service.Models.GuaranteeModel;

namespace JSSATSProject.Service.Service.IService
{
    public interface IGuaranteeService
    {
        public Task<ResponseModel> GetAllAsync();
        public Task<ResponseModel> GetByIdAsync(int id);
        public Task<ResponseModel> GetByProductIdAsync(int productId);
        public Task<ResponseModel> CreateGuaranteeAsync(RequestCreateGuarantee requestGuarantee);
        public Task<ResponseModel> UpdateGuaranteeAsync(int guaranteeId, RequestUpdateGuarantee requestGuarantee);
    }
}