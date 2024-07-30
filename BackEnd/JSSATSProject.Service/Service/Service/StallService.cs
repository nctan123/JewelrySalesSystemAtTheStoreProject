using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.StallModel;
using JSSATSProject.Service.Service.IService;

namespace JSSATSProject.Service.Service.Service;

public class StallService : IStallService
{
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;

    public StallService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseModel> CreateStallAsync(RequestCreateStall requestStall)
    {
        var entity = _mapper.Map<Stall>(requestStall);
        await _unitOfWork.StallRepository.InsertAsync(entity);
        await _unitOfWork.SaveAsync();
        return new ResponseModel
        {
            Data = entity,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> GetAllAsync()
    {
        var entities = await _unitOfWork.StallRepository.GetAsync();
        var response = _mapper.Map<List<ResponseStall>>(entities.ToList());
        return new ResponseModel
        {
            Data = response,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> GetByIdAsync(int id)
    {
        var entity = await _unitOfWork.StallRepository.GetByIDAsync(id);
        var response = _mapper.Map<ResponseStall>(entity);
        return new ResponseModel
        {
            Data = response,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> GetTotalRevenueStallAsync(DateTime startDate, DateTime endDate, int pageIndex, int pageSize, bool ascending)
    {
        try
        {
            
            var stalls = await _unitOfWork.StallRepository.GetAsync(
                includeProperties: "Products.SellOrderDetails.Promotion,Products.SellOrderDetails.Order",
                filter: stall => stall.Products.Any(p => p.SellOrderDetails.Any(od =>
                    od.Order.CreateDate >= startDate &&
                    od.Order.CreateDate <= endDate &&
                    od.Order.Status.Equals(OrderConstants.CompletedStatus) &&
                    od.Status.Equals(SellOrderDetailsConstants.Delivered))),
                orderBy: query =>
                {
                    if (ascending)
                        return query.OrderBy(stall => stall.Name);
                    return query.OrderByDescending(stall => stall.Name);
                },
                pageIndex: pageIndex,
                pageSize: pageSize
            );

         
            var revenuePerStall = stalls
                .Select(stall => new
                {
                    StallName = stall.Name,
                    TotalRevenue = stall.Products
                        .SelectMany(product => product.SellOrderDetails)
                        .Where(od => od.Order.CreateDate >= startDate
                                      && od.Order.CreateDate <= endDate
                                      && od.Order.Status.Equals(OrderConstants.CompletedStatus)
                                      && od.Status.Equals(SellOrderDetailsConstants.Delivered))
                        .Sum(od =>
                        {
                            var unitPriceAfterDiscount = od.Promotion != null
                                ? od.UnitPrice * (1 - od.Promotion.DiscountRate)
                                : od.UnitPrice;
                            if (od.Product.CategoryId == ProductConstants.WholesaleGoldCategory) return unitPriceAfterDiscount;

                            return od.Quantity * unitPriceAfterDiscount;
                        })
                })
                .ToList();

          
            var result = revenuePerStall.Select(item => new Dictionary<string, object>
        {
            { "StallName", item.StallName },
            { "TotalRevenue", item.TotalRevenue }
        }).ToList();

            return new ResponseModel
            {
                Data = result,
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