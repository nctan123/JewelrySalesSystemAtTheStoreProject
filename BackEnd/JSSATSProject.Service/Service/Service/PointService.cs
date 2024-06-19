using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models.PointModel;
using JSSATSProject.Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.Service
{
    public class PointService : IPointService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

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
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetAllAsync()
        {
            var entities = await _unitOfWork.PointRepository.GetAsync();
            var response = _mapper.Map<List<ResponsePoint>>(entities.ToList());
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.PointRepository.GetByIDAsync(id);
            var response = _mapper.Map<ResponsePoint>(entity);
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
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
        
        public async Task<int> GetMaximumApplicablePointForOrder(string customerPhoneNumber, decimal totalOrderPrice)
        {
            var pointObj = await _unitOfWork.PointRepository.GetByCustomerPhoneNumber(customerPhoneNumber);
            var availablePoint = pointObj!.AvailablePoint.GetValueOrDefault();
            var result = totalOrderPrice - availablePoint*1000 > 0 ? availablePoint : totalOrderPrice;
            return Convert.ToInt32(result);
        }

        public async Task<ResponseModel> DecreaseCustomerAvailablePoint(string customerPhoneNumber, int pointValue)
        {
            var pointObj = await _unitOfWork.PointRepository.GetByCustomerPhoneNumber(customerPhoneNumber);
            if (pointObj != null)
            {
                pointObj.AvailablePoint -= pointValue;
                await _unitOfWork.PointRepository.UpdateAsync(pointObj);
            }

            return new ResponseModel()
            {
                Data = pointObj,
                MessageError = ""
            };
        }
    }
}