using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.SymmetryModel;
using JSSATSProject.Service.Service.IService;


namespace JSSATSProject.Service.Service.Service
{
    public class SymmetryService : ISymmetryService
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        public SymmetryService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel> GetAllAsync()
        {
            var entities = await _unitOfWork.SymmetryRepository.GetAsync();


            var response = _mapper.Map<List<ResponseSymmetry>>(entities);

            var responseModel = new ResponseModel
            {
                Data = response,
                MessageError = ""
            };

            return responseModel;
        }
    }
}
