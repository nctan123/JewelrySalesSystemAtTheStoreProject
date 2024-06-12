using JSSATSProject.Repository.Entities;
using JSSATSProject.Repository;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.PaymentModel;
using JSSATSProject.Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JSSATSProject.Service.Models.CustomerModel;
using AutoMapper;

namespace JSSATSProject.Service.Service.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel> CreatePaymentAsync(RequestCreatePayment requestPayment)
        {
            var entity = _mapper.Map<Payment>(requestPayment);
            await _unitOfWork.PaymentRepository.InsertAsync(entity);
            await _unitOfWork.SaveAsync();
            return new ResponseModel
            {
                Data = entity,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetAllAsync()
        {
            var entities = await _unitOfWork.PaymentRepository.GetAsync();
            var response = _mapper.Map<List<ResponsePayment>>(entities.ToList());
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.PaymentRepository.GetByIDAsync(id);
            var response = _mapper.Map<ResponsePayment>(entity);
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> UpdatePaymentAsync(int paymentId, RequestUpdatePayment requestPayment)
        {
            try
            {
                var payment = await _unitOfWork.PaymentRepository.GetByIDAsync(paymentId);
                if (payment != null)
                {

                    _mapper.Map(requestPayment, payment);

                    await _unitOfWork.PaymentRepository.UpdateAsync(payment);

                    return new ResponseModel
                    {
                        Data = payment,
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