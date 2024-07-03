using AutoMapper;
using JSSATSProject.Repository;
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
}