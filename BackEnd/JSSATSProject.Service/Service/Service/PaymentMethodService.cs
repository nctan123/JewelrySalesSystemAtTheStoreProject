using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.PaymentMethodModel;
using JSSATSProject.Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.Service
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentMethodService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel> CreatePaymentMethodAsync(RequestCreatePaymentMethod requestPaymentMethod)
        {
            var entity = _mapper.Map<PaymentMethod>(requestPaymentMethod);
            await _unitOfWork.PaymentMethodRepository.InsertAsync(entity);
            await _unitOfWork.SaveAsync();
            return new ResponseModel
            {
                Data = entity,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetAllAsync()
        {
            var entities = await _unitOfWork.PaymentMethodRepository.GetAsync();
            var response = _mapper.Map<List<ResponsePaymentMethod>>(entities);
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.PaymentMethodRepository.GetByIDAsync(id);
            var response = _mapper.Map<ResponsePaymentMethod>(entity);
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> UpdatePaymentMethodAsync(int paymentmethodid, RequestUpdatePaymentMethod requestPaymentMethod)
        {
            try
            {
                var paymentmethod = await _unitOfWork.PaymentMethodRepository.GetByIDAsync(paymentmethodid);
                if (paymentmethod != null)
                {
                    paymentmethod = _mapper.Map<PaymentMethod>(requestPaymentMethod);
                    await _unitOfWork.PaymentMethodRepository.UpdateAsync(paymentmethod);
                    await _unitOfWork.SaveAsync();

                    return new ResponseModel
                    {
                        Data = paymentmethod,
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