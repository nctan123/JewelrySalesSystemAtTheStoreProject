using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.PolishModel;
using JSSATSProject.Service.Models.ShapeModel;
using JSSATSProject.Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.Service
{
    public class PolishService : IPolishService
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        public PolishService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel> GetAllAsync()
        {
            var entities = await _unitOfWork.PolishRepository.GetAsync();


            var response = _mapper.Map<List<ResponsePolish>>(entities);

            var responseModel = new ResponseModel
            {
                Data = response,
                MessageError = ""
            };

            return responseModel;
        }
    }
}
