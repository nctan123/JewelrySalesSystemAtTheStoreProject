using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.ProductDiamondModel;
using JSSATSProject.Service.Service.IService;

namespace JSSATSProject.Service.Service.Service
{
    public class ProductDiamondService : IProductDiamondService
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        public ProductDiamondService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel> CreateAsync(RequestCreateProductDiamond requestProductDiamond)
        {
            var entity = _mapper.Map<ProductDiamond>(requestProductDiamond);
            await _unitOfWork.ProductDiamondRespository.InsertAsync(entity);
            await _unitOfWork.SaveAsync();
            return new ResponseModel
            {
                Data = entity,
                MessageError = ""
            };
        }
    }
}
