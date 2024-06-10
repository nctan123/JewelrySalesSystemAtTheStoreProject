using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models.ReturnBuyBackPolicyModel;
using JSSATSProject.Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.Service
{
    public class ReturnBuyBackPolicyService : IReturnBuyBackPolicyService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReturnBuyBackPolicyService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel> CreateReturnBuyBackPolicyAsync(RequestCreateReturnBuyBackPolicy requestReturnBuyBackPolicy)
        {
            var entity = _mapper.Map<ReturnBuyBackPolicy>(requestReturnBuyBackPolicy);
            await _unitOfWork.ReturnBuyBackPolicyRepository.InsertAsync(entity);
            await _unitOfWork.SaveAsync();
            return new ResponseModel
            {
                Data = entity,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetAllAsync()
        {
            var entities = await _unitOfWork.ReturnBuyBackPolicyRepository.GetAsync();
            var response = _mapper.Map<List<ResponseReturnBuyBackPolicy>>(entities);
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.ReturnBuyBackPolicyRepository.GetByIDAsync(id);
            var response = _mapper.Map<ResponseReturnBuyBackPolicy>(entity);
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> UpdateReturnBuyBackPolicyAsync(int Id, RequestUpdateReturnBuyBackPolicy requestReturnBuyBackPolicy)
        {
            try
            {
                var returnbuybackpolicy = await _unitOfWork.ReturnBuybackPolicyRepository.GetByIDAsync(Id);
                if (requestReturnBuyBackPolicy != null)
                {

                    _mapper.Map(requestReturnBuyBackPolicy, returnbuybackpolicy);

                    await _unitOfWork.ReturnBuybackPolicyRepository.UpdateAsync(returnbuybackpolicy);

                    return new ResponseModel
                    {
                        Data = returnbuybackpolicy,
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