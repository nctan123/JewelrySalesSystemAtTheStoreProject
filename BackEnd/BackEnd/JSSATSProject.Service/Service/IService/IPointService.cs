using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.PointModel;

namespace JSSATSProject.Service.Service.IService;

public interface IPointService
{
    public Task<ResponseModel> GetAllAsync();
    public Task<ResponseModel> GetByIdAsync(int id);
    public Task<ResponseModel> CreatePointAsync(RequestCreatePoint requestPoint);
    public Task<ResponseModel> UpdatePointAsync(int pointId, RequestUpdatePoint requestPoint);
    public Task<int> GetMaximumApplicablePointForOrder(string customerPhoneNumber, decimal totalOrderPrice);

    public Task<ResponseModel> DecreaseCustomerAvailablePointAsync(string customerPhoneNumber, int pointValue);

    public Task<ResponseModel> AddCustomerPoint(string customerPhoneNumber, decimal orderAmount);

    public  Task<decimal> GetPointToCurrencyConversionRate(DateTime timeStamp);
    //Task DecreaseCustomerAvailablePointAsync(string customerPhone, int discountPoint);
}