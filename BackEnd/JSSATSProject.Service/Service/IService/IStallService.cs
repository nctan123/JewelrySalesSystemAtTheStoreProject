using JSSATSProject.Service.Models.GuaranteeModel;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.StallModel;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.IService
{
    public interface IStallService
    {
        public Task<ResponseModel> GetAllAsync();
        public Task<ResponseModel> GetByIdAsync(int id);
        public Task<ResponseModel> CreateStallAsync(RequestCreateStall requestStall);
    }
}