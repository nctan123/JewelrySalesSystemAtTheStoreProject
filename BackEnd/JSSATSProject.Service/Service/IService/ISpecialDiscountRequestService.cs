using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.SpecialDiscountRequestModel;

namespace JSSATSProject.Service.Service.IService
{
    public interface ISpecialDiscountRequestService
    {
        public Task<ResponseModel> GetAllAsync(bool ascending = true, int pageIndex = 1, int pageSize = 10);
        public Task<SpecialDiscountRequest?> GetEntityByIdAsync(int id);
        public Task<ResponseModel> CreateAsync(CreateSpecialDiscountRequest specialdiscountRequest);
        public Task<ResponseModel> UpdateAsync(int specialdiscountId, UpdateSpecialDiscountRequest specialdiscountRequest);
        public Task<ResponseModel> GetByCustomerIdAsync(int customerId);
        public Task<ResponseModel> GetAsync(string orderCode);
    }
}
