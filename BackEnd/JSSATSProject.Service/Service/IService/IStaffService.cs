using System.Linq.Expressions;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.StaffModel;

namespace JSSATSProject.Service.Service.IService;

public interface IStaffService
{
    public Task<ResponseModel> GetAllSellerAsync(DateTime startDate, DateTime endDate, int pageIndex, int pageSize,
        string sortBy, bool ascending);

    public Task<ResponseModel> CreateStaffAsync(RequestCreateStaff requestStaff);
    public Task<ResponseModel> UpdateStaffAsync(int staffId, RequestUpdateStaff requestStaff);
    public Task<ResponseModel> GetTop6ByDateAsync(DateTime startDate, DateTime endDate);

    public Task<ResponseModel> SearchSellerAsync(string nameSearch, DateTime startDate, DateTime endDate, int pageIndex,
        int pageSize);

    public Task<int?> CountAsync(Expression<Func<Staff, bool>> filter);

    public Task<ResponseModel> GetStaffSummaryAsync(int id, DateTime startDate, DateTime endDate);

    public Task<ResponseModel> GetSellOrdersByStaffIdAsync(int staffId, int pageIndex, int pageSize, DateTime startDate, DateTime endDate);

    public Task<ResponseModel> GetBuyOrdersByStaffIdAsync(int staffId, int pageIndex, int pageSize, DateTime startDate, DateTime endDate);

    public Task<ResponseModel> SearchSellOrdersByStaffIdAsync(int staffId, string orderCode, int pageIndex, int pageSize, DateTime startDate, DateTime endDate);

    public Task<ResponseModel> SearchBuyOrdersByStaffIdAsync(int staffId, string orderCode, int pageIndex, int pageSize,DateTime startDate, DateTime endDate);

    public Task<ResponseModel> GetByIdAsync(int Id);

   

}