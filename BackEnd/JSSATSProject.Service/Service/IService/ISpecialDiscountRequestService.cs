using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.SpecialDiscountRequestModel;

namespace JSSATSProject.Service.Service.IService
{
    public interface ISpecialDiscountRequestService
    {
        public Task<ResponseModel> GetAllAsync();
        public Task<ResponseModel> CreateAsync(CreateSpecialDiscountRequest specialdiscountRequest);
        public Task<ResponseModel> UpdateAsync(int specialdiscountId, UpdateSpecialDiscountRequest specialdiscountRequest);
        public Task<ResponseModel> GetByCustomerIdAsync(int customerId);
    }
}
