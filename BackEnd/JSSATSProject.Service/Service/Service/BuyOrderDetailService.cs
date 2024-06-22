using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Service.Service.IService;

namespace JSSATSProject.Service.Service.Service;

public class BuyOrderDetailService : IBuyOrderDetailService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BuyOrderDetailService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    
}