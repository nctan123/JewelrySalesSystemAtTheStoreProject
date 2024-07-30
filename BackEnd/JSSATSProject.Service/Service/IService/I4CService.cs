using JSSATSProject.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.IService
{
    public interface I4CService
    {
        public Task<ResponseModel> GetCaratAllAsync();
        public Task<ResponseModel> GetCutAllAsync();
        public Task<ResponseModel> GetColorAllAsync();
        public Task<ResponseModel> GetClarityAllAsync();
    }
}
