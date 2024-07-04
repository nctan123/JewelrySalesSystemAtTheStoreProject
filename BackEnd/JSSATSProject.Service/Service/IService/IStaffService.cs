using System.Linq.Expressions;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.StaffModel;

namespace JSSATSProject.Service.Service.IService;

public interface IStaffService
{
    public Task<ResponseModel> GetAllAsync(DateTime startDate, DateTime endDate, int pageIndex, int pageSize,
        string sortBy, bool ascending);

    public Task<ResponseModel> CreateStaffAsync(RequestCreateStaff requestStaff);
    public Task<ResponseModel> UpdateStaffAsync(int staffId, RequestUpdateStaff requestStaff);
    public Task<ResponseModel> GetTop6ByDateAsync(DateTime startDate, DateTime endDate);

    public Task<ResponseModel> SearchAsync(string nameSearch, DateTime startDate, DateTime endDate, int pageIndex,
        int pageSize);

    public Task<int?> CountAsync(Expression<Func<Staff, bool>> filter);
}