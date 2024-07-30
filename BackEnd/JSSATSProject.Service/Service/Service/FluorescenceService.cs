using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.FluorescenceModel;
using JSSATSProject.Service.Service.IService;


namespace JSSATSProject.Service.Service.Service
{
    public class FluorescenceService : IFluorescenceService
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        public FluorescenceService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel> GetAllAsync()
        {
            var entities = await _unitOfWork.FluorescenceRepository.GetAsync();


            var response = _mapper.Map<List<ResponseFluorescence>>(entities);

            var responseModel = new ResponseModel
            {
                Data = response,
                MessageError = ""
            };

            return responseModel;
        }
    }
}
