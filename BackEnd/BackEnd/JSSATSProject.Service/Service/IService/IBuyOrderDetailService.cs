using JSSATSProject.Service.Models;

namespace JSSATSProject.Service.Service.IService;

public interface IBuyOrderDetailService
{
    public Task<ResponseModel> CountProductsSoldByCategoryAsync(DateTime startDate, DateTime endDate);
}