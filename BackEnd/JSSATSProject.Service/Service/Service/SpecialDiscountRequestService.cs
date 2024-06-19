using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Repository.Enums;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.SpecialDiscountRequestModel;
using JSSATSProject.Service.Service.IService;


namespace JSSATSProject.Service.Service.Service
{
    public class SpecialDiscountRequestService : ISpecialDiscountRequestService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;

        public SpecialDiscountRequestService(UnitOfWork unitOfWork, IMapper mapper, ICustomerService customerService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _customerService = customerService;
        }

        public async Task<SpecialDiscountRequest?> GetEntityByIdAsync(int id)
        {
            return await _unitOfWork.SpecialDiscountRequestRepository.GetByIdAsync(id);
        }

        public async Task<ResponseModel> CreateAsync(CreateSpecialDiscountRequest specialdiscountRequest)
        {
            var entity = _mapper.Map<SpecialDiscountRequest>(specialdiscountRequest);
            entity.Staff = await _unitOfWork.StaffRepository.GetByIDAsync(specialdiscountRequest.StaffId);
            entity.Customer = (Customer)(await _customerService.GetEntityByPhoneAsync(specialdiscountRequest.CustomerPhoneNumber.ToString())).Data!;
            entity.Status = SpecialDiscountRequestStatus.Awaiting.ToString();
            
            await _unitOfWork.SpecialDiscountRequestRepository.InsertAsync(entity);
            await _unitOfWork.SaveAsync();

            return new ResponseModel
            {
                Data = entity,
                MessageError = ""
            };
        }

        public async Task<ResponseModel> GetAllAsync()
        {
            var entities = await _unitOfWork.SpecialDiscountRequestRepository.GetAsync(
                includeProperties: "ApprovedByNavigation, Customer,Staff"
            );
            var response = _mapper.Map<List<ResponseSpecialDiscountRequest>>(entities);
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetByCustomerIdAsync(int customerId)
        {
            var entities = await _unitOfWork.SpecialDiscountRequestRepository.GetAsync(
                s => s.CustomerId == customerId && s.SellOrders == null,
                includeProperties: "ApprovedByNavigation,Customer,Staff"
            );

            var response = _mapper.Map<List<ResponseSpecialDiscountRequest>>(entities);

            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }


        public async Task<ResponseModel> UpdateAsync(int specialdiscountId, UpdateSpecialDiscountRequest specialdiscountRequest)
        {
            try
            {
                // Fetch the promotion from the database
                var existingSpecialDiscountRequest = await _unitOfWork.SpecialDiscountRequestRepository.GetByIDAsync(specialdiscountId);

                if (existingSpecialDiscountRequest != null)
                {

                    _mapper.Map(specialdiscountRequest, existingSpecialDiscountRequest);

                    if (specialdiscountRequest.ApprovedBy != null)
                    {
                        var staff = await _unitOfWork.StaffRepository.GetByIDAsync(specialdiscountRequest.ApprovedBy.Value);
                        existingSpecialDiscountRequest.ApprovedByNavigation = staff;
                    }


                    await _unitOfWork.SpecialDiscountRequestRepository.UpdateAsync(existingSpecialDiscountRequest);
                    await _unitOfWork.SaveAsync();

                    return new ResponseModel
                    {
                        Data = existingSpecialDiscountRequest,
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
