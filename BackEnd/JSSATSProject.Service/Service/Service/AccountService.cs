using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.AccountModel;
using JSSATSProject.Service.Service.IService;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.Service
{
    public class AccountService : IAccountService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel> GetAllAsync()
        {
            var entities = await _unitOfWork.AccountRepository.GetAsync();
            var response = _mapper.Map<List<ResponseAccount>>(entities);
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> SignInAsync(RequestSignIn signInModel)
        {
            // Implement the sign-in logic here
            throw new NotImplementedException();
        }

        public async Task<ResponseModel> SignUpAsync(RequestSignUp signUpModel)
        {
            // Implement the sign-up logic here
            throw new NotImplementedException();
        }
    }
}