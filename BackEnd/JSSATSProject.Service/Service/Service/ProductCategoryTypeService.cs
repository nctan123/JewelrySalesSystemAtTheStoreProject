using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.ProductCategoryTypeModel;
using JSSATSProject.Service.Service.IService;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.Service
{
    public class ProductCategoryTypeService : IProductCategoryTypeService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductCategoryTypeService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel> CreateProductCategoryTypeAsync(RequestCreateProductCategoryType requestProductCategoryType)
        {
            var entity = _mapper.Map<ProductCategoryType>(requestProductCategoryType);
            await _unitOfWork.ProductCategoryTypeRepository.InsertAsync(entity);
            await _unitOfWork.SaveAsync();
            return new ResponseModel
            {
                Data = entity,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetAllAsync()
        {
            var entities = await _unitOfWork.ProductCategoryTypeRepository.GetAsync();
            var response = _mapper.Map<List<ResponseProductCategoryType>>(entities.ToList());
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.ProductCategoryTypeRepository.GetByIDAsync(id);
            var response = _mapper.Map<ResponseProductCategoryType>(entity);
            return new ResponseModel
            {
                Data = response,
                MessageError = "",

            };
        }
    }
}