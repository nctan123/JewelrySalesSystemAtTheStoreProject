using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.PaymentDetailModel;
using JSSATSProject.Service.Service.IService;

namespace JSSATSProject.Service.Service.Service;

public class PaymentDetailService : IPaymentDetailService
{
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;

    public PaymentDetailService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseModel> CreatePaymentDetailAsync(RequestCreatePaymentDetail requestPaymentDetail)
    {
        var entity = _mapper.Map<PaymentDetail>(requestPaymentDetail);
        await _unitOfWork.PaymentDetailRepository.InsertAsync(entity);
        await _unitOfWork.SaveAsync();
        return new ResponseModel
        {
            Data = entity,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> GetByPaymentIdAsync(int id)
    {
        var entity = await _unitOfWork.PaymentDetailRepository.GetAsync(
            c => c.PaymentId == id);

        return new ResponseModel
        {
            Data = entity,
            MessageError = ""
        };
    }


    public async Task<ResponseModel> UpdateStatusPaymentDetailAsync(int paymentdetailId,
        RequestUpdatePaymentDetail requestPayment)
    {
        try
        {
            var payment = await _unitOfWork.PaymentRepository.GetByIDAsync(paymentdetailId);
            if (payment != null)
            {
                _mapper.Map(requestPayment, payment);

                await _unitOfWork.PaymentRepository.UpdateAsync(payment);

                return new ResponseModel
                {
                    Data = payment,
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

    public async Task<ResponseModel> UpdateEntityPaymentDetailAsync(PaymentDetail paymentdetail, string status)
    {
        var response = await _unitOfWork.PaymentDetailRepository.UpdateEntityPaymentDetailAsync(paymentdetail, status);
        return new ResponseModel
        {
            Data = response,
            MessageError = ""
        };
    }
}