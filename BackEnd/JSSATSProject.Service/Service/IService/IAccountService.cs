using System.Linq.Expressions;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;

namespace JSSATSProject.Service.Service.IService;

public interface IAccountService
{
    public Task<ResponseModel> GetByUsernameAndPassword(string username, string password);
    public Task<ResponseModel> GetAllAsync(int pageIndex = 1, int pageSize = 10, int? roleId = null);
    public Task<int> CountAsync(Expression<Func<Account, bool>> filter = null);
}