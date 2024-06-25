using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.AccountModel;
using JSSATSProject.Service.Service.IService;


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

        public async Task<ResponseModel> GetByUsernameAndPassword(string username, string password)
        {
            var matchesAccount = await _unitOfWork.AccountRepository.GetByUsernameAndPassword(username, password);
            return new ResponseModel
            {
                Data = matchesAccount,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetAllAsync(int pageIndex = 1, int pageSize = 10)
        {
         
            Func<IQueryable<Account>, IOrderedQueryable<Account>> orderBy = q => q.OrderBy(e => e.RoleId);

          
            var entities = await _unitOfWork.AccountRepository.GetAsync(
                orderBy: orderBy,
                pageIndex: pageIndex,
                pageSize: pageSize
            );

            var response = _mapper.Map<List<ResponseAccount>>(entities);

            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }


    }
}