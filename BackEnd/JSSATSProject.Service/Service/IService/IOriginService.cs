using JSSATSProject.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.IService
{
    public interface IOriginService
    {
        public Task<ResponseModel> GetAllAsync();
    }
}
