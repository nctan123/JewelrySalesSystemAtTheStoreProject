using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.PaymentDetailModel;

namespace JSSATSProject.Service.Service.IService;

public interface IPaymentDetailService
{
    public Task<ResponseModel> GetByPaymentIdAsync(int id);
    public Task<ResponseModel> CreatePaymentDetailAsync(RequestCreatePaymentDetail requestPaymentDetail);

    public Task<ResponseModel> UpdateStatusPaymentDetailAsync(int paymentdetailId,
        RequestUpdatePaymentDetail requestPayment);

    public Task<ResponseModel> UpdateEntityPaymentDetailAsync(PaymentDetail paymentdetail, string status);
}