using System.Linq.Expressions;
using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.StaffModel;
using JSSATSProject.Service.Service.IService;

namespace JSSATSProject.Service.Service.Service;

public class StaffService : IStaffService
{
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;

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
            MessageError = ""
        };
    }

    public async Task<ResponseModel> GetAllAsync(DateTime startDate, DateTime endDate, int pageIndex, int pageSize,
    string sortBy, bool ascending)
    {
        try
        {
            Func<IQueryable<Staff>, IOrderedQueryable<Staff>> orderBy = null;

            switch (sortBy?.ToLower())
            {
                case "totalrevenue":
                    orderBy = ascending
                        ? q => q.OrderBy(s => s.SellOrders
                            .Where(so => so.CreateDate >= startDate && so.CreateDate <= endDate && so.Status == OrderConstants.CompletedStatus)
                            .Sum(so => so.TotalAmount))
                        : q => q.OrderByDescending(s => s.SellOrders
                            .Where(so => so.CreateDate >= startDate && so.CreateDate <= endDate && so.Status == OrderConstants.CompletedStatus)
                            .Sum(so => so.TotalAmount));
                    break;
                case "totalsellorder":
                    orderBy = ascending
                        ? q => q.OrderBy(s => s.SellOrders
                            .Where(so => so.CreateDate >= startDate && so.CreateDate <= endDate && so.Status == OrderConstants.CompletedStatus)
                            .Count())
                        : q => q.OrderByDescending(s => s.SellOrders
                            .Where(so => so.CreateDate >= startDate && so.CreateDate <= endDate && so.Status == OrderConstants.CompletedStatus)
                            .Count());
                    break;
                case "totalbuyorder":
                    orderBy = ascending
                        ? q => q.OrderBy(s => s.BuyOrders
                            .Where(bo => bo.CreateDate >= startDate && bo.CreateDate <= endDate && bo.Status == OrderConstants.CompletedStatus)
                            .Count())
                        : q => q.OrderByDescending(s => s.BuyOrders
                            .Where(bo => bo.CreateDate >= startDate && bo.CreateDate <= endDate && bo.Status == OrderConstants.CompletedStatus)
                            .Count());
                    break;
                default:
                    orderBy = ascending
                        ? q => q.OrderBy(s => s.Lastname).ThenBy(s => s.Firstname)
                        : q => q.OrderByDescending(s => s.Lastname).ThenByDescending(s => s.Firstname);
                    break;
            }

            // Fetch staff entities with SellOrders and BuyOrders included, apply filter, sorting, and pagination
            var entities = await _unitOfWork.StaffRepository.GetAsync(
                orderBy: orderBy,
                pageIndex: pageIndex,
                pageSize: pageSize,
                includeProperties: "SellOrders,BuyOrders");

            // Map entities to response model
            var responseList = entities.Select(entity =>
            {
                var response = _mapper.Map<ResponseStaff>(entity);

                var staffSellOrders = entity.SellOrders
                    .Where(order =>
                        order.CreateDate >= startDate &&
                        order.CreateDate <= endDate &&
                        !string.IsNullOrEmpty(order.Status) &&
                        order.Status.Equals(OrderConstants.CompletedStatus))
                    .ToList();

                var staffBuyOrders = entity.BuyOrders
                    .Where(order =>
                        order.CreateDate >= startDate &&
                        order.CreateDate <= endDate &&
                        !string.IsNullOrEmpty(order.Status) &&
                        order.Status.Equals(OrderConstants.CompletedStatus))
                    .ToList();

                response.TotalRevenue = staffSellOrders.Sum(order => order.TotalAmount);
                response.TotalSellOrder = staffSellOrders.Count;
                response.TotalBuyOrder = staffBuyOrders.Count;

                return response;
            }).ToList();

            var totalCount = await _unitOfWork.StaffRepository.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new ResponseModel
            {
                TotalPages = totalPages,
                TotalElements = totalCount,
                Data = responseList,
                MessageError = ""
            };
        }
        catch (Exception ex)
        {
            return new ResponseModel
            {
                Data = null,
                MessageError = ex.Message
            };
        }
    }



    public async Task<ResponseModel> SearchAsync(string nameSearch, DateTime startDate, DateTime endDate, int pageIndex,
    int pageSize)
    {
        // Define filter based on name search
        Expression<Func<Staff, bool>> filter = null;
        if (!string.IsNullOrEmpty(nameSearch))
            filter = staff =>
                staff.Firstname.Contains(nameSearch) ||
                staff.Lastname.Contains(nameSearch);

        // Define ordering by total sell orders count
        Func<IQueryable<Staff>, IOrderedQueryable<Staff>> orderBy = q => q.OrderByDescending(s => s.SellOrders.Count);

        // Fetch staff entities with SellOrders and BuyOrders included, apply filter, sorting, and pagination
        var staffEntities = await _unitOfWork.StaffRepository.GetAsync(
            filter,
            orderBy,
            pageIndex: pageIndex,
            pageSize: pageSize,
            includeProperties: "SellOrders,BuyOrders");

        // Map entities to response model
        var responseList = staffEntities.Select(entity =>
        {
            var response = _mapper.Map<ResponseStaff>(entity);

            // Filter SellOrders by date range and status, including null checks
            var staffSellOrders = entity.SellOrders
                .Where(order =>
                    order.CreateDate >= startDate &&
                    order.CreateDate <= endDate &&
                    !string.IsNullOrEmpty(order.Status) &&
                    order.Status.Equals(OrderConstants.CompletedStatus))
                .ToList();

            // Filter BuyOrders by date range and status, including null checks
            var staffBuyOrders = entity.BuyOrders
                .Where(order =>
                    order.CreateDate >= startDate &&
                    order.CreateDate <= endDate &&
                    !string.IsNullOrEmpty(order.Status) &&
                    order.Status.Equals(OrderConstants.CompletedStatus))
                .ToList();

            // Calculate TotalRevenue, TotalSellOrder, and TotalBuyOrder
            response.TotalRevenue = staffSellOrders.Sum(order => order.TotalAmount);
            response.TotalSellOrder = staffSellOrders.Count;
            response.TotalBuyOrder = staffBuyOrders.Count;

            return response;
        }).ToList();

        var totalCount = await _unitOfWork.StaffRepository.CountAsync(filter);
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new ResponseModel
        {
            TotalPages = totalPages,
            TotalElements = totalCount,
            Data = responseList,
            MessageError = ""
        };
    }


    public async Task<int?> CountAsync(Expression<Func<Staff, bool>> filter)
    {
        return await _unitOfWork.StaffRepository.CountAsync(filter);
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

    public async Task<ResponseModel> GetTop6ByDateAsync(DateTime startDate, DateTime endDate)
    {
        // Fetch orders within the specified date range
        var orders = await _unitOfWork.SellOrderRepository.GetAsync(
            o => o.CreateDate >= startDate && o.CreateDate <= endDate &&
                 o.Status.Equals(OrderConstants.CompletedStatus));

        // Group orders by StaffId and calculate total revenue for each group
        var groupedOrders = orders
            .GroupBy(o => o.StaffId)
            .Select(group => new
            {
                StaffId = group.Key,
                TotalRevenue = group.Sum(o => o.TotalAmount)
            })
            .OrderByDescending(g => g.TotalRevenue)
            .ToList();

        // Get the top 5 staff members by revenue
        var top5 = groupedOrders.Take(5).ToList();
        var otherRevenue = groupedOrders.Skip(5).Sum(g => g.TotalRevenue);

        // Initialize result list
        var result = new List<Dictionary<string, object>>();

        // Add top 5 staff members to result
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

        // Add other revenue to result
        result.Add(new Dictionary<string, object>
        {
            { "StaffId", 0 },
            { "Firstname", "Other" },
            { "Lastname", "" },
            { "TotalRevenue", otherRevenue }
        });

        // Return the response model with the result data
        return new ResponseModel
        {
            Data = result
        };
    }

    public async Task<ResponseModel> GetByIdAsync(int id, DateTime startDate, DateTime endDate)
    {
        try
        {
            // Fetch the staff entity with SellOrders and BuyOrders included, filter by ID
            var entities = await _unitOfWork.StaffRepository.GetAsync(
                filter: s => s.Id == id,
                includeProperties: "SellOrders,BuyOrders");

            if (entities == null)
            {
                return new ResponseModel
                {
                    Data = null,
                    MessageError = "Staff not found"
                };
            }


            var responseList = entities.Select(entity =>
            {
                var response = _mapper.Map<ResponseStaff>(entity);

                var staffSellOrders = entity.SellOrders
                    .Where(order =>
                        order.CreateDate >= startDate &&
                        order.CreateDate <= endDate &&
                        !string.IsNullOrEmpty(order.Status) &&
                        order.Status.Equals(OrderConstants.CompletedStatus))
                    .ToList();

                var staffBuyOrders = entity.BuyOrders
                    .Where(order =>
                        order.CreateDate >= startDate &&
                        order.CreateDate <= endDate &&
                        !string.IsNullOrEmpty(order.Status) &&
                        order.Status.Equals(OrderConstants.CompletedStatus))
                    .ToList();

                response.TotalRevenue = staffSellOrders.Sum(order => order.TotalAmount);
                response.TotalSellOrder = staffSellOrders.Count;
                response.TotalBuyOrder = staffBuyOrders.Count;

                return response;
            }).ToList();

            return new ResponseModel
            {
                Data = responseList,
                MessageError = ""
            };
        }
        catch (Exception ex)
        {
            return new ResponseModel
            {
                Data = null,
                MessageError = ex.Message
            };
        }
    }



}