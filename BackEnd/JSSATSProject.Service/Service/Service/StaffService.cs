using System.Linq.Expressions;
using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Repository.CustomLib;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.BuyOrderDetailModel;
using JSSATSProject.Service.Models.BuyOrderModel;
using JSSATSProject.Service.Models.OrderModel;
using JSSATSProject.Service.Models.SellOrderDetailsModel;
using JSSATSProject.Service.Models.StaffModel;
using JSSATSProject.Service.Service.IService;

namespace JSSATSProject.Service.Service.Service;

public class StaffService : IStaffService
{
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;
    private readonly IPointService _pointService;

    public StaffService(UnitOfWork unitOfWork, IMapper mapper, IPointService pointService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        this._pointService = pointService;
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

    public async Task<ResponseModel> GetAllSellerAsync(DateTime startDate, DateTime endDate, int pageIndex,
        int pageSize,
        string sortBy, bool ascending)
    {
        try
        {
            Func<IQueryable<Staff>, IOrderedQueryable<Staff>> entitiesOrderBy = null;

            switch (sortBy?.ToLower())
            {
                case "totalrevenue":
                    entitiesOrderBy = ascending
                        ? q => q.OrderBy(s => s.SellOrders
                            .Where(so =>
                                so.CreateDate >= startDate && so.CreateDate <= endDate &&
                                so.Status == OrderConstants.CompletedStatus)
                            .Sum(so => so.TotalAmount))
                        : q => q.OrderByDescending(s => s.SellOrders
                            .Where(so =>
                                so.CreateDate >= startDate && so.CreateDate <= endDate &&
                                so.Status == OrderConstants.CompletedStatus)
                            .Sum(so => so.TotalAmount));
                    break;
                case "totalsellorder":
                    entitiesOrderBy = ascending
                        ? q => q.OrderBy(s => s.SellOrders
                            .Count(so =>
                                so.CreateDate >= startDate && so.CreateDate <= endDate &&
                                so.Status == OrderConstants.CompletedStatus))
                        : q => q.OrderByDescending(s => s.SellOrders
                            .Count(so =>
                                so.CreateDate >= startDate && so.CreateDate <= endDate &&
                                so.Status == OrderConstants.CompletedStatus));
                    break;
                case "totalbuyorder":
                    entitiesOrderBy = ascending
                        ? q => q.OrderBy(s => s.BuyOrders
                            .Count(bo =>
                                bo.CreateDate >= startDate && bo.CreateDate <= endDate &&
                                bo.Status == OrderConstants.CompletedStatus))
                        : q => q.OrderByDescending(s => s.BuyOrders
                            .Count(bo =>
                                bo.CreateDate >= startDate && bo.CreateDate <= endDate &&
                                bo.Status == OrderConstants.CompletedStatus));
                    break;
                default:
                    entitiesOrderBy = ascending
                        ? q => q.OrderBy(s => s.Lastname).ThenBy(s => s.Firstname)
                        : q => q.OrderByDescending(s => s.Lastname).ThenByDescending(s => s.Firstname);
                    break;
            }

            Func<IQueryable<ResponseStaff>, IOrderedQueryable<ResponseStaff>> orderBy = null;

            switch (sortBy.ToLower())
            {
                case "totalrevenue":
                    orderBy = ascending
                        ? q => q.OrderBy(s => s.TotalRevenue).ThenBy(s => s.Status)
                        : q => q.OrderByDescending(s => s.TotalRevenue).ThenBy(s => s.Status);
                    break;
                case "totalsellorder":
                    orderBy = ascending
                        ? q => q.OrderBy(s => s.TotalSellOrder).ThenBy(s => s.Status)
                        : q => q.OrderByDescending(s => s.TotalSellOrder).ThenBy(s => s.Status);
                    break;
                case "totalbuyorder":
                    orderBy = ascending
                        ? q => q.OrderBy(s => s.TotalBuyOrder).ThenBy(s => s.Status)
                        : q => q.OrderByDescending(s => s.TotalBuyOrder).ThenBy(s => s.Status);
                    break;
                default:
                    orderBy = ascending
                        ? q => q.OrderBy(s => s.Lastname).ThenBy(s => s.Firstname).ThenBy(s => s.Status)
                        : q => q.OrderByDescending(s => s.Lastname).ThenByDescending(s => s.Firstname).ThenBy(s => s.Status);
                    break;
            }

           
            var entities = await _unitOfWork.StaffRepository.GetAsync(
                filter: s => s.Account.Role.Name == RoleConstants.Seller,
                orderBy: entitiesOrderBy,
                pageIndex: pageIndex,
                pageSize: pageSize,
                includeProperties: "SellOrders,BuyOrders, SellOrders.SpecialDiscountRequest");

            var pointToCurrencyConversionRate = await _pointService.GetPointToCurrencyConversionRate(CustomLibrary.NowInVietnamTime());


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

            
                    response.TotalRevenue = staffSellOrders
                        .Sum(order =>
                        {
                            decimal discountRate = order.SpecialDiscountRequest?.DiscountRate ?? 0;
                            return order.TotalAmount * (1 - discountRate) - order.DiscountPoint * pointToCurrencyConversionRate;
                        });

                    response.TotalSellOrder = staffSellOrders.Count;
                    response.TotalBuyOrder = staffBuyOrders.Count;

                    return response;
                })
                .AsQueryable();

            responseList = orderBy(responseList);

            var totalCount =
                await _unitOfWork.StaffRepository.CountAsync(filter: s => s.Account.Role.Name == RoleConstants.Seller);
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


    public async Task<ResponseModel> SearchSellerAsync(string nameSearch, DateTime startDate, DateTime endDate,
        int pageIndex, int pageSize)
    {
       
        Expression<Func<Staff, bool>> filter = staff => staff.Account.Role.Name == RoleConstants.Seller;

       
        if (!string.IsNullOrEmpty(nameSearch))
        {
            filter = staff =>
                staff.Account.Role.Name == RoleConstants.Seller &&
                (staff.Firstname.Contains(nameSearch) || staff.Lastname.Contains(nameSearch));
        }

        
        Func<IQueryable<Staff>, IOrderedQueryable<Staff>> orderBy = q => q.OrderByDescending(s => s.SellOrders.Count);

       
        var staffEntities = await _unitOfWork.StaffRepository.GetAsync(
            filter: filter,
            orderBy: orderBy,
            pageIndex: pageIndex,
            pageSize: pageSize,
            includeProperties: "SellOrders,BuyOrders,Account.Role");

        
        var responseList = staffEntities.Select(entity =>
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


                if (requestStaff.Status?.ToLower() == "inactive")
                {

                    if (staff.Account != null)
                    {
                        staff.Account.Status = "inactive";
                        await _unitOfWork.AccountRepository.UpdateAsync(staff.Account);
                    }
                }
                else
                {
                    if (staff.Account != null)
                    {
                        staff.Account.Status = "active";
                        await _unitOfWork.AccountRepository.UpdateAsync(staff.Account);
                    }
                }


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
            return new ResponseModel
            {
                Data = null,
                MessageError = "An error occurred while updating the staff: " + ex.Message
            };
        }
    }


    public async Task<ResponseModel> GetTop6ByDateAsync(DateTime startDate, DateTime endDate)
    {
        
        var orders = await _unitOfWork.SellOrderRepository.GetAsync(
            o => o.CreateDate >= startDate
                 && o.CreateDate <= endDate
                 && o.Status.Equals(OrderConstants.CompletedStatus),
            includeProperties: "SpecialDiscountRequest"
        );

        var pointToCurrencyConversionRate = await _pointService.GetPointToCurrencyConversionRate(CustomLibrary.NowInVietnamTime());


        var groupedOrders = orders
            .GroupBy(o => o.StaffId)
            .Select(group => new
            {
                StaffId = group.Key,
                TotalRevenue = group.Sum(order =>
                {
                    decimal discountRate = order.SpecialDiscountRequest?.DiscountRate ?? 0;
                    return order.TotalAmount * (1 - discountRate) - order.DiscountPoint * pointToCurrencyConversionRate;
                })
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


    public async Task<ResponseModel> GetStaffSymmaryAsync(int id, DateTime startDate, DateTime endDate)
    {
        var staff = (await _unitOfWork.StaffRepository.GetAsync(
            filter: s => s.Id == id,
            includeProperties: "SellOrders,BuyOrders")).FirstOrDefault();

        var pointToCurrencyConversionRate = await _pointService.GetPointToCurrencyConversionRate(CustomLibrary.NowInVietnamTime());

        if (staff == null)
        {
            return new ResponseModel
            {
                Data = null
            };
        }

        var responseStaff = new ResponseStaff
        {
            Id = staff.Id,
            AccountId = staff.AccountId,
            Firstname = staff.Firstname,
            Lastname = staff.Lastname,
            Phone = staff.Phone,
            Email = staff.Email,
            Address = staff.Address,
            Gender = staff.Gender,
            Status = staff.Status,
            TotalRevenue = staff.SellOrders
                .Where(so =>
                    so.CreateDate >= startDate && so.CreateDate <= endDate &&
                    so.Status == OrderConstants.CompletedStatus)
                .Sum(so => so.TotalAmount - so.DiscountPoint * pointToCurrencyConversionRate),
            TotalSellOrder = staff.SellOrders
                .Count(so =>
                    so.CreateDate >= startDate && so.CreateDate <= endDate &&
                    so.Status == OrderConstants.CompletedStatus),
            TotalBuyOrder = staff.BuyOrders
                .Count(bo =>
                    bo.CreateDate >= startDate && bo.CreateDate <= endDate &&
                    bo.Status == OrderConstants.CompletedStatus)
        };

        return new ResponseModel
        {
            Data = responseStaff,
        };
    }

    public async Task<ResponseModel> GetSellOrdersByStaffIdAsync(int staffId, int pageIndex, int pageSize,
        DateTime startDate, DateTime endDate)
    {
        try
        {
            var staffEntity = (await _unitOfWork.StaffRepository.GetAsync(
                    s => s.Id == staffId,
                    includeProperties:
                    "SellOrders,SellOrders.SellOrderDetails,SellOrders.SpecialDiscountRequest," +
                    "SellOrders.Payments.PaymentDetails.PaymentMethod,SellOrders.SellOrderDetails.Product,SellOrders.SellOrderDetails.Promotion"))
                .FirstOrDefault();

            if (staffEntity == null)
            {
                return new ResponseModel
                {
                    Data = null,
                    MessageError = "Staff not found",
                    TotalPages = 0,
                    TotalElements = 0
                };
            }

            var filteredOrders = staffEntity.SellOrders
                .Where(order => order.CreateDate >= startDate && order.CreateDate <= endDate &&
                                order.Status == OrderConstants.CompletedStatus);

            var totalCount = filteredOrders.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var paginatedOrders = filteredOrders
                .OrderByDescending(order => order.CreateDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var sellOrders = new List<ResponseSellOrder>();
            foreach (var order in paginatedOrders)
            {
                var responseSellOrder = _mapper.Map<ResponseSellOrder>(order);

                // Calculate final price
                responseSellOrder.FinalAmount = await GetFinalPriceAsync(order);

                responseSellOrder.SellOrderDetails =
                    _mapper.Map<List<ResponseSellOrderDetails>>(order.SellOrderDetails);

                sellOrders.Add(responseSellOrder);
            }

            return new ResponseModel
            {
                TotalPages = totalPages,
                TotalElements = totalCount,
                Data = sellOrders,
                MessageError = ""
            };
        }
        catch (Exception ex)
        {
            
            return new ResponseModel
            {
                Data = null,
                MessageError = $"An error occurred: {ex.Message}",
                TotalPages = 0,
                TotalElements = 0
            };
        }
    }

    public async Task<ResponseModel> GetBuyOrdersByStaffIdAsync(int staffId, int pageIndex, int pageSize,
        DateTime startDate, DateTime endDate)
    {
        try
        {
            var staffEntity = (await _unitOfWork.StaffRepository.GetAsync(
                    s => s.Id == staffId,
                    includeProperties:
                    "BuyOrders,BuyOrders.BuyOrderDetails," +
                    "BuyOrders.BuyOrderDetails.PurchasePriceRatio," +
                    "BuyOrders.BuyOrderDetails.Material,BuyOrders.BuyOrderDetails.CategoryType"))
                .FirstOrDefault();

            if (staffEntity == null)
            {
                return new ResponseModel
                {
                    Data = null,
                    MessageError = "Staff not found",
                    TotalPages = 0,
                    TotalElements = 0
                };
            }

            var filteredOrders = staffEntity.BuyOrders
                .Where(order => order.CreateDate >= startDate && order.CreateDate <= endDate &&
                                order.Status == OrderConstants.CompletedStatus);

            var totalCount = filteredOrders.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var paginatedOrders = filteredOrders
                .OrderByDescending(order => order.CreateDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var buyOrders = new List<ResponseBuyOrder>();
            foreach (var order in paginatedOrders)
            {
                var responseBuyOrder = _mapper.Map<ResponseBuyOrder>(order);

               
                responseBuyOrder.BuyOrderDetails =
                    _mapper.Map<List<ResponseBuyOrderDetail>>(order.BuyOrderDetails);

                buyOrders.Add(responseBuyOrder);
            }

            return new ResponseModel
            {
                TotalPages = totalPages,
                TotalElements = totalCount,
                Data = buyOrders,
                MessageError = ""
            };
        }
        catch (Exception ex)
        {
        
            return new ResponseModel
            {
                Data = null,
                MessageError = $"An error occurred: {ex.Message}",
                TotalPages = 0,
                TotalElements = 0
            };
        }
    }

    public async Task<decimal> GetFinalPriceAsync(SellOrder sellOrder)
    {
        var pointRate = await _unitOfWork.CampaignPointRepository.GetPointRate(DateTime.Now);
        var discountPoint = sellOrder.DiscountPoint;
        var specialDiscountRequest = sellOrder.SpecialDiscountRequest;
        var specialDiscountRate = (specialDiscountRequest?.DiscountRate).GetValueOrDefault(0);
        if (specialDiscountRequest?.Status == SpecialDiscountRequestConstants.RejectedStatus) specialDiscountRate = 0;
        decimal finalPrice = (sellOrder!.TotalAmount - discountPoint * pointRate) * (1 - specialDiscountRate);
        return finalPrice;
    }

    public async Task<ResponseModel> SearchSellOrdersByStaffIdAsync(int staffId, string orderCode, int pageIndex,
        int pageSize, DateTime startDate, DateTime endDate)
    {
        try
        {
            var staff = await _unitOfWork.StaffRepository.GetAsync(
                s => s.Id == staffId,
                includeProperties:
                "SellOrders,SellOrders.SellOrderDetails,SellOrders.SpecialDiscountRequest," +
                "SellOrders.Payments.PaymentDetails.PaymentMethod,SellOrders.SellOrderDetails.Product,SellOrders.SellOrderDetails.Promotion");

            var staffEntity = staff.FirstOrDefault();

            if (staffEntity == null)
            {
                return new ResponseModel
                {
                    Data = null,
                    MessageError = "Staff not found",
                    TotalPages = 0,
                    TotalElements = 0
                };
            }

            var sellOrders = staffEntity.SellOrders
                .Where(order => order.Code.Contains(orderCode) &&
                                order.CreateDate >= startDate &&
                                order.CreateDate <= endDate &&
                                order.Status == OrderConstants.CompletedStatus)
                .ToList();

            var totalCount = sellOrders.Count;
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var paginatedOrders = sellOrders
                .OrderByDescending(order => order.CreateDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var responseSellOrders = new List<ResponseSellOrder>();
            foreach (var order in paginatedOrders)
            {
                var responseSellOrder = _mapper.Map<ResponseSellOrder>(order);

                // Calculate final price
                responseSellOrder.FinalAmount = await GetFinalPriceAsync(order);

                // Map sell order details
                responseSellOrder.SellOrderDetails =
                    _mapper.Map<List<ResponseSellOrderDetails>>(order.SellOrderDetails);

                responseSellOrders.Add(responseSellOrder);
            }

            return new ResponseModel
            {
                TotalPages = totalPages,
                TotalElements = totalCount,
                Data = responseSellOrders,
                MessageError = ""
            };
        }
        catch (Exception ex)
        {
         
            return new ResponseModel
            {
                Data = null,
                MessageError = $"An error occurred: {ex.Message}",
                TotalPages = 0,
                TotalElements = 0
            };
        }
    }

    public async Task<ResponseModel> SearchBuyOrdersByStaffIdAsync(int staffId, string orderCode, int pageIndex,
        int pageSize, DateTime startDate, DateTime endDate)
    {
        try
        {
            var staff = await _unitOfWork.StaffRepository.GetAsync(
                s => s.Id == staffId,
                includeProperties: "BuyOrders,BuyOrders.BuyOrderDetails," +
                                   "BuyOrders.BuyOrderDetails.PurchasePriceRatio," +
                                   "BuyOrders.BuyOrderDetails.Material,BuyOrders.BuyOrderDetails.CategoryType");

            var staffEntity = staff.FirstOrDefault();

            if (staffEntity == null)
            {
                return new ResponseModel
                {
                    Data = null,
                    MessageError = "Staff not found",
                    TotalPages = 0,
                    TotalElements = 0
                };
            }

            var buyOrders = staffEntity.BuyOrders
                .Where(order => order.Code.Contains(orderCode) &&
                                order.CreateDate >= startDate &&
                                order.CreateDate <= endDate &&
                                order.Status == OrderConstants.CompletedStatus)
                .ToList();

            var totalCount = buyOrders.Count;
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var paginatedOrders = buyOrders
                .OrderByDescending(order => order.CreateDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var responseBuyOrders = new List<ResponseBuyOrder>();
            foreach (var order in paginatedOrders)
            {
                var responseBuyOrder = _mapper.Map<ResponseBuyOrder>(order);

                responseBuyOrder.BuyOrderDetails =
                    _mapper.Map<List<ResponseBuyOrderDetail>>(order.BuyOrderDetails);

                responseBuyOrders.Add(responseBuyOrder);
            }

            return new ResponseModel
            {
                TotalPages = totalPages,
                TotalElements = totalCount,
                Data = responseBuyOrders,
                MessageError = ""
            };
        }
        catch (Exception ex)
        {
            
            return new ResponseModel
            {
                Data = null,
                MessageError = $"An error occurred: {ex.Message}",
                TotalPages = 0,
                TotalElements = 0
            };
        }
    }

    public async Task<ResponseModel> GetByIdAsync(int Id)
    {
        var response = await _unitOfWork.StaffRepository.GetByIDAsync(Id);
        return new ResponseModel
        {
            Data = response,
        };
    }
}