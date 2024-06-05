using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models.Material;
using JSSATSProject.Service.Service.IService;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.Service
{
    public class MaterialService : IMaterialService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MaterialService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel> CreateMaterialAsync(RequestCreateMaterial requestMaterial)
        {
            var entity = _mapper.Map<Material>(requestMaterial);
            await _unitOfWork.MaterialRepository.InsertAsync(entity);
            await _unitOfWork.SaveAsync();
            return new ResponseModel
            {
                Data = entity,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetAllAsync()
        {
            var entities = await _unitOfWork.MaterialRepository.GetAsync();
            var response = _mapper.Map<List<ResponseMaterial>>(entities.ToList());
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.MaterialRepository.GetByIDAsync(id);
            var response = _mapper.Map<ResponseMaterial>(entity);
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }
    }
}