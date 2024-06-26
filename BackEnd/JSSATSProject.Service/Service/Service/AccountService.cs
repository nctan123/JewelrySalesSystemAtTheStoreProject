using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.AccountModel;
using JSSATSProject.Service.Service.IService;
using System.Linq.Expressions;


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

        public async Task<ResponseModel> GetAllAsync(int pageIndex = 1, int pageSize = 10, int? roleId = null)
        {
            try
            {
                // Define filter condition based on roleId
                Expression<Func<Account, bool>> filter = a => !roleId.HasValue || a.RoleId == roleId.Value;

                // Define sorting logic (always ascending by StaffName)
                Func<IQueryable<Account>, IOrderedQueryable<Account>> orderBy = q => q.OrderBy(e => e.Staff.Firstname);

                // Fetch data with filtering and sorting
                var entities = await _unitOfWork.AccountRepository.GetAsync(
                    filter: filter,
                    orderBy: orderBy,
                    pageIndex: pageIndex,
                    pageSize: pageSize,
                    includeProperties: "Staff"
                );

                // Map the entities to the response DTO
                var response = _mapper.Map<List<ResponseAccount>>(entities);

                // Return the response model
                return new ResponseModel
                {
                    Data = response,
                    MessageError = "",
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Data = null,
                    MessageError = ex.Message,
                };
            }
        }



    }
}