﻿using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models.DiamondPriceListModel;
using JSSATSProject.Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.Service
{
    public class DiamondPriceListService : IDiamondPriceListService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DiamondPriceListService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel> CreateDiamondPriceListAsync(RequestCreateDiamondPriceList requestDiamondPriceList)
        {
            var entity = _mapper.Map<DiamondPriceList>(requestDiamondPriceList);
            await _unitOfWork.DiamondPriceListRepository.InsertAsync(entity);
            await _unitOfWork.SaveAsync();
            return new ResponseModel
            {
                Data = entity,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetAllAsync()
        {
            var entities = await _unitOfWork.DiamondPriceListRepository.GetAsync();
            var response = _mapper.Map<List<ResponseDiamondPriceList>>(entities.ToList());
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.DiamondPriceListRepository.GetByIDAsync(id);
            var response = _mapper.Map<ResponseDiamondPriceList>(entity);
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> UpdateDiamondPriceListAsync(int diamondpricelistId, RequestUpdateDiamondPriceList requestDiamondPriceList)
        {
            try
            {
                var diamondpricelist = await _unitOfWork.DiamondPriceListRepository.GetByIDAsync(diamondpricelistId);
                if (diamondpricelist != null)
                {

                    _mapper.Map(requestDiamondPriceList, diamondpricelist);

                    await _unitOfWork.DiamondPriceListRepository.UpdateAsync(diamondpricelist);

                    return new ResponseModel
                    {
                        Data = diamondpricelist,
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