using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.OrderModel;
using JSSATSProject.Service.Models.ProductModel;
using JSSATSProject.Service.Service.IService;

namespace JSSATSProject.Service.Service.Service;

public class BuyOrderService : IBuyOrderService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BuyOrderService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public Task<ResponseModel> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel> GetByProductIdAsync(int productId)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel> CreateAsync(RequestCreateBuyOrder entity)
    {
        throw new NotImplementedException();
    }

    public async Task<BuyOrder?> GetEntityByCodeAsync(string code)
    {
        return await _unitOfWork.BuyOrderRepository.GetByCodeAsync(code);
    }
    
}