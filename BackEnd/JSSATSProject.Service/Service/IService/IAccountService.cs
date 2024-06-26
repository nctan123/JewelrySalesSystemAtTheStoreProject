using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.AccountModel;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.IService
{
    public interface IAccountService
    {
        public Task<ResponseModel> GetByUsernameAndPassword(string username, string password);
        public Task<ResponseModel> GetAllAsync(int pageIndex = 1, int pageSize = 10);
    }
}