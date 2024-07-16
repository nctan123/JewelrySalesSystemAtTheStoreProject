using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models._4CModel;
using JSSATSProject.Service.Models.AccountModel;
using JSSATSProject.Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.Service
{
    public class _4CService : I4CService
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        public _4CService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseModel> GetCaratAllAsync()
        {
            var entities = await _unitOfWork.CaratRepository.GetAsync();

           
            var response = _mapper.Map<List<ResponseCarat>>(entities);

            var responseModel = new ResponseModel
            {
                Data = response,
                MessageError = ""
            };
          
            return responseModel;
        }

        public async Task<ResponseModel> GetClarityAllAsync()
        {
            var entities = await _unitOfWork.ClarityRepository.GetAsync();


            var response = _mapper.Map<List<ResponseClarity>>(entities);

            var responseModel = new ResponseModel
            {
                Data = response,
                MessageError = ""
            };

            return responseModel;
        }

        public async Task<ResponseModel> GetColorAllAsync()
        {
            var entities = await _unitOfWork.ColorRepository.GetAsync();


            var response = _mapper.Map<List<ResponseColor>>(entities);

            var responseModel = new ResponseModel
            {
                Data = response,
                MessageError = ""
            };

            return responseModel;
        }

        public async Task<ResponseModel> GetCutAllAsync()
        {
            var entities = await _unitOfWork.CutRepository.GetAsync();


            var response = _mapper.Map<List<ResponseCut>>(entities);

            var responseModel = new ResponseModel
            {
                Data = response,
                MessageError = ""
            };

            return responseModel;
        }
    }
}
