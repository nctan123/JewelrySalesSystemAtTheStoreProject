﻿using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models.GuaranteeModel;
using JSSATSProject.Service.Service.IService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.Service
{
    public class GuaranteeService : IGuaranteeService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GuaranteeService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel> CreateGuaranteeAsync(RequestCreateGuarantee requestGuarantee)
        {
            var entity = _mapper.Map<Guarantee>(requestGuarantee);
            await _unitOfWork.GuaranteeRepository.InsertAsync(entity);
            await _unitOfWork.SaveAsync();
            return new ResponseModel
            {
                Data = entity,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetAllAsync()
        {
            var entities = await _unitOfWork.GuaranteeRepository.GetAsync();
            var response = _mapper.Map<List<ResponseGuarantee>>(entities);
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.GuaranteeRepository.GetByIDAsync(id);
            var response = _mapper.Map<ResponseGuarantee>(entity);
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> UpdateGuaranteeAsync(int guaranteeId, RequestUpdateGuarantee requestGuarantee)
        {
            try
            {
                var guarantee = await _unitOfWork.GuaranteeRepository.GetByIDAsync(guaranteeId);
                if (guarantee != null)
                {

                    _mapper.Map(requestGuarantee, guarantee);

                    await _unitOfWork.GuaranteeRepository.UpdateAsync(guarantee);

                    return new ResponseModel
                    {
                        Data = guarantee,
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