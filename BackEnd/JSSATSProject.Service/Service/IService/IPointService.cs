using JSSATSProject.Service.Models.GuaranteeModel;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.PointModel;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.IService
{
    public interface IPointService
    {
        public Task<ResponseModel> GetAllAsync();
        public Task<ResponseModel> GetByIdAsync(int id);
        public Task<ResponseModel> CreatePointAsync(RequestCreatePoint requestPoint);
        public Task<ResponseModel> UpdatePointAsync(int pointId, RequestUpdatePoint requestPoint);
        public Task<int> GetMaximumApplicablePointForOrder(string customerPhoneNumber, decimal totalOrderPrice);

        public Task<ResponseModel> DecreaseCustomerAvailablePoint(string customerPhoneNumber, int pointValue);
    }
}