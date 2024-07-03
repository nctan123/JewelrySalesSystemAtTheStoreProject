using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.PurchasePriceRatioModel;
using JSSATSProject.Service.Service.IService;

namespace JSSATSProject.Service.Service.Service;

public class PurchasePriceRatioService : IPurchasePriceRatioService
{
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;

    public PurchasePriceRatioService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseModel> CreateAsync(RequestCreatePurchasePriceRatio requestCreatePurchasePriceRatio)
    {
        var entity = _mapper.Map<PurchasePriceRatio>(requestCreatePurchasePriceRatio);
        await _unitOfWork.PurchasePriceRatioRepository.InsertAsync(entity);
        await _unitOfWork.SaveAsync();
        return new ResponseModel
        {
            Data = entity,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> GetAllAsync()
    {
        var entities = await _unitOfWork.PurchasePriceRatioRepository.GetAsync();
        return new ResponseModel
        {
            Data = entities,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> GetByIdAsync(int id)
    {
        var response = await _unitOfWork.PurchasePriceRatioRepository.GetByIDAsync(id);
        return new ResponseModel
        {
            MessageError = "",
            Data = response
        };
    }
}