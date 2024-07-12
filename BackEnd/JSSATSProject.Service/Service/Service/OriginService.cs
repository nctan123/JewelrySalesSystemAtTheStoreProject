using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models._4CModel;
using JSSATSProject.Service.Models.OriginModel;
using JSSATSProject.Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.Service
{
    public class OriginService : IOriginService
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        public OriginService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseModel> GetAllAsync()
        {
            var entities = await _unitOfWork.OriginRepository.GetAsync();


            var response = _mapper.Map<List<ResponseOrigin>>(entities);

            var responseModel = new ResponseModel
            {
                Data = response,
                MessageError = ""
            };

            return responseModel;
        }
    }
}
