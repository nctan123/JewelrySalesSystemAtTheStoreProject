using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.StallTypeModel;
using JSSATSProject.Service.Service.IService;

namespace JSSATSProject.Service.Service.Service;

public class StallTypeService : IStallTypeService
{
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;

    public StallTypeService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseModel> CreateStallTypeAsync(RequestCreateStallType requestStallType)
    {
        var entity = _mapper.Map<StallType>(requestStallType);
        await _unitOfWork.StallTypeRepository.InsertAsync(entity);
        await _unitOfWork.SaveAsync();
        return new ResponseModel
        {
            Data = entity,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> GetAllAsync()
    {
        var entities = await _unitOfWork.StallTypeRepository.GetAsync();
        var response = _mapper.Map<List<ResponseStallType>>(entities.ToList());
        return new ResponseModel
        {
            Data = response,
            MessageError = ""
        };
    }

    public async Task<ResponseModel> GetByIdAsync(int id)
    {
        var entity = await _unitOfWork.StallRepository.GetByIDAsync(id);
        var response = _mapper.Map<ResponseStallType>(entity);
        return new ResponseModel
        {
            Data = response,
            MessageError = ""
        };
    }
}