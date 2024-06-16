using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Service.IService;

namespace JSSATSProject.Service.Service.Service;

public class PurchasePriceRatioService : IPurchasePriceRatioService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PurchasePriceRatioService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseModel> GetAllAsync()
    {
        var entities = await _unitOfWork.PurchasePriceRatioRepository.GetAsync();
        return new ResponseModel
        {
            Data = entities,
            MessageError = "",
        };
    }

    public async Task<ResponseModel> GetByIdAsync(int id)
    {
        var response = await _unitOfWork.PurchasePriceRatioRepository.GetByIDAsync(
            id
        );


        return new ResponseModel
        {
            MessageError = "",
            Data = response
        };
    }
    
}