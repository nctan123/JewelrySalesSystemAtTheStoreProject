using System.Linq.Expressions;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.PromotionModel;

namespace JSSATSProject.Service.Service.IService;

public interface IPromotionService
{
    public Task<ResponseModel> GetAllAsync(int pageIndex, int pageSize, bool ascending);
    public Task<ResponseModel> CreatePromotionAsync(RequestCreatePromotion requestPromotion);
    public Task<ResponseModel> UpdatePromotionAsync(int promotionId, RequestUpdatePromotion requestPromotion);
    public Task<ResponseModel> SearchAsync(string searchTerm, int pageIndex, int pageSize);
    public Task<int> CountAsync(Expression<Func<Promotion, bool>> filter = null);

    public Task<ResponseModel> GetByIdAsync(int promotionId);
}