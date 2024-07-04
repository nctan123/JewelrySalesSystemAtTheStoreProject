using System.Linq.Expressions;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.CustomerModel;

namespace JSSATSProject.Service.Service.IService;

public interface ICustomerService
{
    public Task<ResponseModel> GetAllAsync(int pageIndex, int pageSize);
    public Task<ResponseModel> SearchAsync(string searchTerm, int pageIndex = 1, int pageSize = 10);
    public Task<ResponseModel> GetByPhoneAsync(string phoneNumber);
    public Task<ResponseModel> GetEntityByPhoneAsync(string phoneNumber);
    public Task<ResponseModel> CreateCustomerAsync(RequestCreateCustomer requestCustomer);
    public Task<ResponseModel> UpdateCustomerAsync(int customerId, RequestUpdateCustomer requestCustomer);
    public Task<ResponseModel> CountNewCustomer(DateTime startDate, DateTime endDate);
    public Task<int> CountAsync(Expression<Func<Customer, bool>> filter = null);
    public Task<ResponseModel> GetSellOrdersByPhoneAsync(string phoneNumber, int pageIndex, int pageSize);
    public Task<ResponseModel> GetPaymentsByPhoneAsync(string phoneNumber, int pageIndex, int pageSize);
    public Task<ResponseModel> GetBuyOrdersByPhoneAsync(string phoneNumber, int pageIndex, int pageSize);
}