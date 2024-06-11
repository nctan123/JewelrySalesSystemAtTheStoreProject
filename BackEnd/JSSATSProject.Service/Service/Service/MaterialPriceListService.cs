using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.MaterialPriceListModel;
using JSSATSProject.Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.Service
{
    public class MaterialPriceListService : IMaterialPriceListService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MaterialPriceListService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel> CreateMaterialPriceListAsync(RequestCreateMaterialPriceList requestMaterialPriceList)
        {
            var entity = _mapper.Map<MaterialPriceList>(requestMaterialPriceList);
            await _unitOfWork.MaterialPriceListRepository.InsertAsync(entity);
            await _unitOfWork.SaveAsync();
            return new ResponseModel
            {
                Data = entity,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetAllAsync()
        {
            var entities = await _unitOfWork.MaterialPriceListRepository.GetAsync();
            var response = _mapper.Map<List<ResponseMaterialPriceList>>(entities.ToList());
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.MaterialPriceListRepository.GetByIDAsync(id);
            var response = _mapper.Map<ResponseMaterialPriceList>(entity);
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> UpdateMaterialPriceListAsync(int materialpricelistId, RequestUpdateMaterialPriceList requestMaterialPriceList)
        {
            try
            {
                var materialpricelist = await _unitOfWork.MaterialPriceListRepository.GetByIDAsync(materialpricelistId);
                if (materialpricelist != null)
                {
                    materialpricelist = _mapper.Map<MaterialPriceList>(requestMaterialPriceList);
                    await _unitOfWork.MaterialPriceListRepository.UpdateAsync(materialpricelist);
                    await _unitOfWork.SaveAsync();

                    return new ResponseModel
                    {
                        Data = materialpricelist,
                        MessageError = "",
                    };
                }

                return new ResponseModel
                {
                    Data = null,
                    MessageError = "Not Found",
                };
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate error response
                return new ResponseModel
                {
                    Data = null,
                    MessageError = "An error occurred while updating the customer: " + ex.Message
                };
            }
        }
    }
}