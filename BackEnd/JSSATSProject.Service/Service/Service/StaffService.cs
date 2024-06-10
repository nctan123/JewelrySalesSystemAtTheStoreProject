using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.StaffModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.Service
{
    public class StaffService : IStaffService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StaffService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel> CreateStaffAsync(RequestCreateStaff requestStaff)
        {
            var entity = _mapper.Map<Staff>(requestStaff);
            await _unitOfWork.StaffRepository.InsertAsync(entity);
            await _unitOfWork.SaveAsync();
            return new ResponseModel
            {
                Data = entity,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetAllAsync()
        {
            var entities = await _unitOfWork.StaffRepository.GetAsync();
            var response = _mapper.Map<List<ResponseStaff>>(entities);
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetAllByDateAsync(DateTime startDate, DateTime endDate)
        {
            var entities = await _unitOfWork.StaffRepository.GetAsync(includeProperties: "Orders");
            var response = _mapper.Map<List<ResponseStaff>>(entities);

            
            foreach (var staff in response)
            {
                staff.TotalRevennue = entities
                    .Where(entity => entity.Id == staff.Id) 
                    .SelectMany(entity => entity.Orders)
                    .Where(order => order.CreateDate >= startDate && order.CreateDate <= endDate)
                    .Sum(order => order.TotalAmount);
            }

            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }



        public async Task<ResponseModel> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.StaffRepository.GetByIDAsync(id);
            var response = _mapper.Map<ResponseStaff>(entity);
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> UpdateStaffAsync(int staffId, RequestUpdateStaff requestStaff)
        {
            try
            {
                var staff = await _unitOfWork.StaffRepository.GetByIDAsync(staffId);
                if (staff != null)
                {

                    _mapper.Map(requestStaff, staff);

                    await _unitOfWork.StaffRepository.UpdateAsync(staff);

                    return new ResponseModel
                    {
                        Data = staff,
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