using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Service.IService;

namespace JSSATSProject.Service.Service.Service;

public class BuyOrderDetailService : IBuyOrderDetailService
{
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;

    public BuyOrderDetailService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseModel> CountProductsSoldByCategoryAsync(DateTime startDate, DateTime endDate)
    {
        var buyOrders = await _unitOfWork.BuyOrderRepository.GetAsync(
            bo => bo.CreateDate >= startDate
                  && bo.CreateDate <= endDate
                  && bo.Status.Equals(OrderConstants.CompletedStatus),
            includeProperties: "BuyOrderDetails.CategoryType");

        var buyOrderDetails = buyOrders.SelectMany(bo => bo.BuyOrderDetails);

        var productsSoldPerCategory = buyOrderDetails
            .GroupBy(od => od.CategoryType.Name)
            .Select(group => new
            {
                Category = group.Key,
                Quantity = group.Sum(od => od.Quantity)
            })
            .ToList();

        var result = productsSoldPerCategory.Select(item => new Dictionary<string, object>
    {
        { "Category", item.Category },
        { "Quantity", item.Quantity }
    }).ToList();

        return new ResponseModel
        {
            Data = result
        };
    }

}