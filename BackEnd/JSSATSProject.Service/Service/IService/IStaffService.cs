using JSSATSProject.Service.Models.GuaranteeModel;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.StaffModel;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.IService
{
    public interface IStaffService
    {
        public Task<ResponseModel> GetAllByDateAsync(DateTime startDate, DateTime endDate);
        public Task<ResponseModel> GetAllAsync();
        public Task<ResponseModel> GetByIdAsync(int id);
        public Task<ResponseModel> CreateStaffAsync(RequestCreateStaff requestStaff);
        public Task<ResponseModel> UpdateStaffAsync(int staffId, RequestUpdateStaff requestStaff);
    }
}