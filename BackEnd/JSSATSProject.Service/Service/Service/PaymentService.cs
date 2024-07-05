using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Repository.CustomLib;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.PaymentModel;
using JSSATSProject.Service.Service.IService;
using System.Linq.Expressions;

namespace JSSATSProject.Service.Service.Service;

public class PaymentService : IPaymentService
{
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;

    public PaymentService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseModel> CreatePaymentAsync(RequestCreatePayment requestPayment)
    {
        var entity = _mapper.Map<Payment>(requestPayment);
        entity.CreateDate = CustomLibrary.NowInVietnamTime();
        await _unitOfWork.PaymentRepository.InsertAsync(entity);
        await _unitOfWork.SaveAsync();
        return new ResponseModel
        {
            Data = entity,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> GetAllAsync(int pageIndex, int pageSize)
    {
        // Define sorting order by creation date (always descending)
        Func<IQueryable<Payment>, IOrderedQueryable<Payment>> orderBy = q => q.OrderByDescending(p => p.CreateDate);

        // Fetch payment entities with sorting and pagination
        var entities = await _unitOfWork.PaymentRepository.GetAsync(
            orderBy: orderBy,
            pageIndex: pageIndex,
            pageSize: pageSize,
            includeProperties: "Order,PaymentDetails");

        // Map entities to response model
        var responseList = _mapper.Map<List<ResponsePayment>>(entities.ToList());

        var totalCount = await _unitOfWork.PaymentRepository.CountAsync();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new ResponseModel
        {
            TotalPages = totalPages,
            TotalElements = totalCount,
            Data = responseList,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> GetByIdAsync(int id)
    {
        var entity = await _unitOfWork.PaymentRepository.GetByIDAsync(id);
        var response = _mapper.Map<ResponsePayment>(entity);
        return new ResponseModel
        {
            Data = response,
            MessageError = ""
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

    public async Task<int> GetOrderIdByPaymentIdAsync(int id)
    {
        var payment = await _unitOfWork.PaymentRepository.GetByIDAsync(id);
        return payment.OrderId;
    }
    public async Task<decimal> GetTotal(int paymentMethod, DateTime startDate, DateTime endDate)
    {
        // Define filter to get completed payments with amount > 0, specific payment method, within the date range,
        // and with PaymentDetails having status as completed
        Expression<Func<Payment, bool>> filter = payment =>
            payment.Status == PaymentConstants.CompletedStatus &&
            payment.Amount > 0 &&
            payment.CreateDate >= startDate &&
            payment.CreateDate <= endDate &&
            payment.PaymentDetails.Any(pd => pd.PaymentMethodId == paymentMethod && pd.Status == PaymentConstants.CompletedStatus);

        // Fetch payments that match the filter criteria
        var payments = await _unitOfWork.PaymentRepository.GetAsync(filter);

        // Calculate the sum of amounts
        var totalSum = payments.Sum(payment => payment.Amount);

        return totalSum;
    }



}