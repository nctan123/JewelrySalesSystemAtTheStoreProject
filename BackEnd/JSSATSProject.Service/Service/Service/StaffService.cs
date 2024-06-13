using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.StaffModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ResponseModel> GetDetailsByDateAsync(int id, DateTime? startDate, DateTime? endDate)
        {
            var entity = await _unitOfWork.StaffRepository.GetAsync(filter: e => e.Id == id, includeProperties: "Orders");
            var staffEntity = entity.FirstOrDefault();

            if (staffEntity == null)
            {
                return new ResponseModel
                {
                    Data = null,
                    MessageError = "Staff not found",
                };
            }

            var response = _mapper.Map<ResponseStaff>(staffEntity);

            var staffOrders = staffEntity.Orders
                .Where(order => order.CreateDate >= startDate && order.CreateDate <= endDate);

            response.TotalRevennue = staffOrders.Sum(order => order.TotalAmount);
            response.TotalOrder = staffOrders.Count();

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

        public async Task<ResponseModel> GetTop6ByMonthAsync(int month)
        {
            var orders = await _unitOfWork.OrderRepository.GetAsync(
                filter: o => o.CreateDate.Month == month && o.CreateDate.Year == DateTime.Now.Year);

            var groupedOrders = orders
                .GroupBy(o => o.StaffId)
                .Select(group => new
                {
                    StaffId = group.Key,
                    TotalRevenue = group.Sum(o => o.TotalAmount)
                })
                .OrderByDescending(g => g.TotalRevenue)
                .ToList();

            var top5 = groupedOrders.Take(5).ToList();
            var otherRevenue = groupedOrders.Skip(5).Sum(g => g.TotalRevenue);

            var result = new List<Dictionary<string, object>>();

            foreach (var staff in top5)
            {
                var staffDetails = await _unitOfWork.StaffRepository.GetByIDAsync(staff.StaffId);
                result.Add(new Dictionary<string, object>
                {
                    { "StaffId", staff.StaffId },
                    { "Firstname", staffDetails.Firstname },
                    { "Lastname", staffDetails.Lastname },
                    { "TotalRevenue", staff.TotalRevenue }
                });
            }

            result.Add(new Dictionary<string, object>
            {
                { "StaffId", 0 },
                { "Firstname", "Other" },
                { "Lastname", "" },
                { "TotalRevenue", otherRevenue }
            });

            return new ResponseModel
            {
                Data = result
            };
        }

    }
}