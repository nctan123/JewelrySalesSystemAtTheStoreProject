using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models.ProductCategoryModel;
using JSSATSProject.Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.Service
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductCategoryService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel> CreateProductCategoryAsync(RequestCreateProductCategory requestProductCategory)
        {
            var entity = _mapper.Map<ProductCategory>(requestProductCategory);
            await _unitOfWork.ProductCategoryRepository.InsertAsync(entity);
            await _unitOfWork.SaveAsync();
            return new ResponseModel
            {
                Data = entity,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetAllAsync()
        {
            var entities = await _unitOfWork.ProductCategoryRepository.GetAsync();
            var response = _mapper.Map<List<ResponseProductCategory>>(entities.ToList());
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.ProductCategoryRepository.GetByIDAsync(id);
            var response = _mapper.Map<ResponseProductCategory>(entity);
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }
    }
}