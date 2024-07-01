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

    public async Task<ResponseModel> GetByReturnBuyBackPolicyIdAsync(int Id)
    {
        var entities = await _unitOfWork.PurchasePriceRatioRepository.GetAsync(
            filter: x => x.ReturnbuybackpolicyId == Id
        );

        var responseEntities = _mapper.Map<IEnumerable<ResponsePurchasePriceRatio>>(entities);

        return new ResponseModel
        {
            Data = responseEntities,
            MessageError = ""
        };
    }



}