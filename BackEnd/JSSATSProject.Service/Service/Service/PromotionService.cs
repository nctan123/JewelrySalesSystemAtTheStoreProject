using System.Linq.Expressions;
using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.PromotionModel;
using JSSATSProject.Service.Service.IService;

namespace JSSATSProject.Service.Service.Service;

public class PromotionService : IPromotionService
{
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;

    public PromotionService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseModel> CreatePromotionAsync(RequestCreatePromotion requestPromotion)
    {
        var entity = _mapper.Map<Promotion>(requestPromotion);


        if (requestPromotion.CategoriIds.Any())
        {
            var categoryIds = requestPromotion.CategoriIds.ToList();
            var categories = await _unitOfWork.ProductCategoryRepository
                .GetAsync(pc => categoryIds.Contains(pc.Id));


            foreach (var category in categories) entity.Categories.Add(category);
        }


        await _unitOfWork.PromotionRepository.InsertAsync(entity);
        await _unitOfWork.SaveAsync();


        return new ResponseModel
        {
            Data = entity,
            MessageError = string.Empty
        };
    }

    public async Task<ResponseModel> GetAllAsync(int pageIndex, int pageSize, bool ascending)
    {
        Expression<Func<Promotion, bool>> filter = pc => pc.Status == "active" || pc.Status == "inactive";


        Func<IQueryable<Promotion>, IOrderedQueryable<Promotion>> orderBy = q => q
            .OrderByDescending(pc => pc.Status == "active")
            .ThenByDescending(pc => pc.EndDate);


        var entities = await _unitOfWork.PromotionRepository.GetAsync(
            filter,
            ascending
                ? q => q.OrderBy(pc => pc.StartDate).ThenBy(pc => pc.EndDate)
                : q => q.OrderByDescending(pc => pc.StartDate).ThenByDescending(pc => pc.EndDate),
            "Categories",
            pageIndex,
            pageSize
        );

        var response = _mapper.Map<List<ResponsePromotion>>(entities);
        return new ResponseModel
        {
            Data = response,
            MessageError = ""
        };
    }


    public async Task<ResponseModel> UpdatePromotionAsync(int promotionId, RequestUpdatePromotion requestPromotion)
    {
        try
        {
            var promotion = await _unitOfWork.PromotionRepository.GetByIDAsync(promotionId);
            if (promotion != null)
            {
                _mapper.Map(requestPromotion, promotion);

                await _unitOfWork.PromotionRepository.UpdateAsync(promotion);

                return new ResponseModel
                {
                    Data = promotion,
                    MessageError = ""
                };
            }

            return new ResponseModel
            {
                Data = null,
                MessageError = "Not Found"
            };
        }
        catch (Exception ex)
        {
            // Log the exception and return an appropriate error response
            return new ResponseModel
            {
                Data = null,
                MessageError = "An error occurred while updating the customer: " + ex.Message
            };
        }
    }

    public async Task<ResponseModel> SearchAsync(string searchTerm, int pageIndex, int pageSize)
    {
        try
        {
            Expression<Func<Promotion, bool>> filter = p =>
                string.IsNullOrEmpty(searchTerm) ||
                p.Name.Contains(searchTerm) ||
                p.Categories.Any(c => c.Name.Contains(searchTerm));


            var entities = await _unitOfWork.PromotionRepository.GetAsync(
                filter,
                null,
                "Categories",
                pageIndex,
                pageSize
            );

            var totalCount = await _unitOfWork.PromotionRepository.CountAsync(filter);
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var response = _mapper.Map<List<ResponsePromotion>>(entities);
            return new ResponseModel
            {
                TotalPages = totalPages,
                TotalElements = totalCount,
                Data = response,
                MessageError = ""
            };
        }
        catch (Exception ex)
        {
            return new ResponseModel
            {
                Data = null,
                MessageError = ex.Message
            };
        }
    }

    public async Task<int> CountAsync(Expression<Func<Promotion, bool>> filter = null)
    {
        return await _unitOfWork.PromotionRepository.CountAsync(filter);
    }
}