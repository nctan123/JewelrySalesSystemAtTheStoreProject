using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.SpecialDiscountRequestModel;

namespace JSSATSProject.Service.Service.IService
{
    public interface ISpecialDiscountRequestService
    {
        public Task<ResponseModel> GetAllAsync();
        public Task<ResponseModel> CreateSpecialDiscountRequestAsync(CreateSpecialDiscountRequest specialdiscountRequest);
        public Task<ResponseModel> UpdateSpecialDiscountRequestAsync(int specialdiscountId, UpdateSpecialDiscountRequest specialdiscountRequest);
        public Task<ResponseModel> GetByCustomerIdAsync(int customerId);
    }
}
