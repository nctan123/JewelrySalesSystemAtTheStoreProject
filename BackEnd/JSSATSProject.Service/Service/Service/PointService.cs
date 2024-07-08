using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.PointModel;
using JSSATSProject.Service.Service.IService;

namespace JSSATSProject.Service.Service.Service;

public class PointService : IPointService
{
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;

    public PointService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<ResponseModel> CreatePointAsync(RequestCreatePoint requestPoint)
    {
        var entity = _mapper.Map<Point>(requestPoint);
        await _unitOfWork.PointRepository.InsertAsync(entity);
        await _unitOfWork.SaveAsync();
        return new ResponseModel
        {
            Data = entity,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> GetAllAsync()
    {
        var entities = await _unitOfWork.PointRepository.GetAsync();
        var response = _mapper.Map<List<ResponsePoint>>(entities.ToList());
        return new ResponseModel
        {
            Data = response,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> GetByIdAsync(int id)
    {
        var entity = await _unitOfWork.PointRepository.GetByIDAsync(id);
        var response = _mapper.Map<ResponsePoint>(entity);
        return new ResponseModel
        {
            Data = response,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> UpdatePointAsync(int pointId, RequestUpdatePoint requestPoint)
    {
        try
        {
            var point = await _unitOfWork.PointRepository.GetByIDAsync(pointId);
            if (point != null)
            {
                _mapper.Map(requestPoint, point);

                await _unitOfWork.PointRepository.UpdateAsync(point);

                return new ResponseModel
                {
                    Data = point,
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

    public async Task<int> GetMaximumApplicablePointForOrder(string customerPhoneNumber, decimal totalOrderPrice)
    {
        var pointObj = await _unitOfWork.PointRepository.GetByCustomerPhoneNumber(customerPhoneNumber);
        var availablePoint = pointObj!.AvailablePoint.GetValueOrDefault();
        var rate = await _unitOfWork.CampaignPointRepository.GetPointRate(DateTime.Now);

        if (totalOrderPrice / rate <= availablePoint) return (int)(totalOrderPrice * rate);
        var result = totalOrderPrice - availablePoint * rate > 0 ? availablePoint : totalOrderPrice * rate;
        return Convert.ToInt32(result);
    }

    public async Task<ResponseModel> DecreaseCustomerAvailablePointAsync(string customerPhoneNumber, int pointValue)
    {
        var pointObj = await _unitOfWork.PointRepository.GetByCustomerPhoneNumber(customerPhoneNumber);
        if (pointObj != null)
        {
            pointObj.AvailablePoint -= pointValue;
            await _unitOfWork.PointRepository.UpdateAsync(pointObj);
        }

        return new ResponseModel
        {
            Data = pointObj,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> AddCustomerPoint(string customerPhoneNumber, decimal orderAmount)
    {
        var pointObj = await _unitOfWork.PointRepository.GetByCustomerPhoneNumber(customerPhoneNumber);
        var accumulativePointRate = await _unitOfWork.CampaignPointRepository.GetOrderValueToPointConversionRate(DateTime.Now);
        var pointToCurrencyRate = await _unitOfWork.CampaignPointRepository.GetPointRate(DateTime.Now);


        var pointToAdd = (int)Math.Floor(orderAmount * accumulativePointRate * pointToCurrencyRate);

        if (pointObj != null)
        {
            pointObj.AvailablePoint += pointToAdd;
            pointObj.Totalpoint += pointToAdd;
            await _unitOfWork.PointRepository.UpdateAsync(pointObj);
        }

        return new ResponseModel
        {
            Data = pointObj,
            MessageError = ""
        };
    }
}