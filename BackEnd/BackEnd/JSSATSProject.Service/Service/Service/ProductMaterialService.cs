using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.ProductMaterialModel;
using JSSATSProject.Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.Service
{
    public class ProductMaterialService : IProductMaterialService
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        public ProductMaterialService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel> CreateAsync(RequestCreateProductMaterial requestProductMaterial)
        {
            var entity = _mapper.Map<ProductMaterial>(requestProductMaterial);
            await _unitOfWork.ProductMaterialRepository.InsertAsync(entity);
            await _unitOfWork.SaveAsync();
            return new ResponseModel
            {
                Data = entity,
                MessageError = ""
            };
        }

        public async Task<ResponseModel> UpdateAsync(RequestUpdateProductMaterial requestProductMaterial)
        {
            var entities = await _unitOfWork.ProductMaterialRepository.GetAsync(pm =>
                pm.MaterialId == requestProductMaterial.MaterialId && pm.ProductId == requestProductMaterial.ProductId);
            var entity = entities.FirstOrDefault();
            if (entity == null)
            {
                return new ResponseModel
                {
                    Data = null,
                    MessageError = "ProductMaterial not found"
                };
            }

            _mapper.Map(requestProductMaterial, entity);
            await _unitOfWork.ProductMaterialRepository.UpdateAsync(entity);
            await _unitOfWork.SaveAsync();

            return new ResponseModel
            {
                Data = entity,
                MessageError = ""
            };
        }


    }
}
