using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.PromotionRequestModel;
using JSSATSProject.Service.Service.IService;


namespace JSSATSProject.Service.Service.Service
{
    public class PromotionRequestService : IPromotionRequestService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PromotionRequestService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseModel> CreatePromotionRequestAsync(CreatePromotionRequest promotionRequest)
        {
            var entity = _mapper.Map<PromotionRequest>(promotionRequest);

            if (promotionRequest.CategoriIds.Any())
            {
                var categoryIds = promotionRequest.CategoriIds.ToList();
                var categories = await _unitOfWork.ProductCategoryRepository
                                                  .GetAsync(pc => categoryIds.Contains(pc.Id) && pc.Status == "active");



                foreach (var category in categories)
                {
                    entity.Categories.Add(category);
                }
            }

            await _unitOfWork.PromotionRequestRepository.InsertAsync(entity);
            await _unitOfWork.SaveAsync();


            return new ResponseModel
            {
                Data = entity,
                MessageError = string.Empty,
            };
        }
           

        public async Task<ResponseModel> GetAllAsync()
        {
            var entities = await _unitOfWork.PromotionRequestRepository.GetAsync(
                includeProperties: "ApprovedByNavigation, Manager,Categories"
            );
            var response = _mapper.Map<List<ResponsePromotionRequest>>(entities);
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> UpdatePromotionRequestAsync(int promotionrequestId, UpdatePromotionRequest promotionRequest)
        {
            try
            {
                // Fetch the promotion from the database
                var existingPromotionRequest = await _unitOfWork.PromotionRequestRepository.GetByIDAsync(promotionrequestId);

                if (existingPromotionRequest != null)
                {
                    
                    _mapper.Map(promotionRequest, existingPromotionRequest);

                    if (promotionRequest.ApprovedBy != null)
                    {
                        var staff = await _unitOfWork.StaffRepository.GetByIDAsync(promotionRequest.ApprovedBy.Value);
                        existingPromotionRequest.ApprovedByNavigation = staff;
                    }


                    await _unitOfWork.PromotionRequestRepository.UpdateAsync(existingPromotionRequest);
                    await _unitOfWork.SaveAsync();

                    return new ResponseModel
                    {
                        Data = existingPromotionRequest,
                        MessageError = "",
                    };
                }

                return new ResponseModel
                {
                    Data = null,
                    MessageError = "Promotion not found",
                };
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate error response
                return new ResponseModel
                {
                    Data = null,
                    MessageError = "An error occurred while updating the promotion: " + ex.Message
                };
            }
        }
    }
}
