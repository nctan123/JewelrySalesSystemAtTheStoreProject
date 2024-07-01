using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.PromotionRequestModel;

namespace JSSATSProject.Service.Service.IService;

public interface IPromotionRequestService
{
    public Task<ResponseModel> GetAllAsync(int pageIndex, int pageSize, bool ascending);
    public Task<ResponseModel> CreatePromotionRequestAsync(CreatePromotionRequest promotionRequest);

    public Task<ResponseModel> UpdatePromotionRequestAsync(int promotionrequestId,
        UpdatePromotionRequest promotionRequest);

    public Task<ResponseModel> SearchAsync(string searchTerm, int pageIndex, int pageSize);
}