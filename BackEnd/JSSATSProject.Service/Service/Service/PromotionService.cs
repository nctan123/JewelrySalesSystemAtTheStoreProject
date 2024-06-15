using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.PromotionModel;
using JSSATSProject.Service.Service.IService;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.Service
{
    public class PromotionService : IPromotionService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

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
                                                  .GetAsync(pc => categoryIds.Contains(pc.Id) && pc.Status == "active");



                foreach (var category in categories)
                {
                    entity.Categories.Add(category);
                }
            }


            await _unitOfWork.PromotionRepository.InsertAsync(entity);
            await _unitOfWork.SaveAsync();

            
            return new ResponseModel
            {
                Data = entity,
                MessageError = string.Empty,  
            };
        }


        public async Task<ResponseModel> GetAllAsync()
        {
            var entities = await _unitOfWork.PromotionRepository.GetAsync(
                includeProperties: "Categories"
                );
            var response = _mapper.Map<List<ResponsePromotion>>(entities);
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.PromotionRepository.GetByIDAsync(id);
            var response = _mapper.Map<ResponsePromotion>(entity);
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
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
                        MessageError = "",
                    };
                }

                return new ResponseModel
                {
                    Data = null,
                    MessageError = "Not Found",
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

      

    }
}