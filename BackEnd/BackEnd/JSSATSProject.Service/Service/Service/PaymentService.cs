using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Repository.CustomLib;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.PaymentModel;
using JSSATSProject.Service.Service.IService;
using System.Linq.Expressions;
using JSSATSProject.Service.Extensions;

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
            includeProperties: "Sellorder,Buyorder,PaymentDetails,Customer,PaymentDetails.PaymentMethod");

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
        // Fetch payment entities with the required related properties
        var entities = await _unitOfWork.PaymentRepository.GetAsync(
            filter: p => p.Id == id,
            includeProperties: "Sellorder,Buyorder,PaymentDetails,Customer,PaymentDetails.PaymentMethod"
        );

        // Get the first matching entity (or null if none found)
        var entity = entities.FirstOrDefault();

        // If entity is not found, return an error message
        if (entity == null)
        {
            return new ResponseModel
            {
                Data = null,
                MessageError = "Payment not found"
            };
        }

        // Map the entity to the response model
        var response = _mapper.Map<ResponsePayment>(entity);

        // Return the response model
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


    public async Task<int?> GetSellOrderIdByPaymentIdAsync(int id)
    {
        var payment = await _unitOfWork.PaymentRepository.GetByIDAsync(id);
        if (payment.SellorderId == null) return null;
        return payment.SellorderId;
    }

    public async Task<int?> GetBuyOrderIdByPaymentIdAsync(int id)
    {
        var payment = await _unitOfWork.PaymentRepository.GetByIDAsync(id);
        if (payment.BuyorderId == null) return null;
        return payment.BuyorderId;
    }

    public async Task<ResponseModel> GetTotalAllPayMentAsync(DateTime startDate, DateTime endDate, int order)
    {
        Expression<Func<Payment, bool>> baseFilter = payment =>
            payment.Status == PaymentConstants.CompletedStatus &&
            payment.Amount > 0 &&
            payment.CreateDate >= startDate &&
            payment.CreateDate <= endDate;

        // Adjust filter based on the order parameter
        Expression<Func<Payment, bool>> specificFilter;
        if (order == 1) // For BuyOrder
        {
            specificFilter = payment => payment.BuyorderId != null;
        }
        else if (order == 2) // For SellOrder
        {
            specificFilter = payment => payment.SellorderId != null;
        }
        else
        {
            throw new ArgumentException("Invalid order value. Use 1 for BuyOrder or 2 for SellOrder.");
        }

        var filter = baseFilter.AndAlso(specificFilter);

        var payments = await _unitOfWork.PaymentRepository.GetAsync(
            filter,
            includeProperties: "PaymentDetails.PaymentMethod"
        );

        var paymentMethodTotalSums = payments
            .SelectMany(payment => payment.PaymentDetails)
            .Where(pd => pd.Status == PaymentConstants.CompletedStatus)
            .GroupBy(pd => pd.PaymentMethod.Name)
            .Select(group => new
            {
                PaymentMethodName = group.Key,
                TotalAmount = group.Sum(pd => pd.Payment.Amount)
            })
            .ToList();

        var result = new List<Dictionary<string, object>>();

        foreach (var paymentMethodSum in paymentMethodTotalSums)
        {
            result.Add(new Dictionary<string, object>
        {
            { "PaymentMethodName", paymentMethodSum.PaymentMethodName },
            { "TotalAmount", paymentMethodSum.TotalAmount }
        });
        }

        // Return the response model with the result data
        return new ResponseModel
        {
            Data = result
        };
    }
}