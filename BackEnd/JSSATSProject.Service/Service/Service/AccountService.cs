using System.Linq.Expressions;
using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.AccountModel;
using JSSATSProject.Service.Service.IService;

namespace JSSATSProject.Service.Service.Service;

public class AccountService : IAccountService
{
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;

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
            MessageError = ""
        };
    }

    public async Task<ResponseModel> GetAllAsync(int pageIndex = 1, int pageSize = 10, int? roleId = null)
    {
        try
        {
           
            Expression<Func<Account, bool>> filter = a => !roleId.HasValue || a.RoleId == roleId.Value;


            Func<IQueryable<Account>, IOrderedQueryable<Account>> orderBy = q => q
            .OrderBy(e => e.Staff.Status)
            .ThenBy(e => e.Staff.Firstname);


            // Fetch data with filtering and sorting
            var entities = await _unitOfWork.AccountRepository.GetAsync(
                filter,
                orderBy,
                pageIndex: pageIndex,
                pageSize: pageSize,
                includeProperties: "Staff,Role"
            );

            // Map the entities to the response DTO
            var response = _mapper.Map<List<ResponseAccount>>(entities);

            var result = new ResponseModel
            {
                Data = response,
                MessageError = ""
            };
            result.TotalElements = await CountAsync(a => a.RoleId == roleId);
            result.TotalPages = result.CalculateTotalPageCount(pageSize);
            // Return the response model
            return result;
        }
        catch (Exception ex)
        {
            return new ResponseModel
            {
                Data = null,
                MessageError = ex.Message
            };
        }
    }

    public async Task<int> CountAsync(Expression<Func<Account, bool>> filter = null)
    {
        return await _unitOfWork.AccountRepository.CountAsync(filter);
    }

    public async Task<ResponseModel> SearchAsync( int pageIndex = 1,int pageSize = 10,int? roleId = null,string searchTerm = null)
    {
        try
        {
            // Define filter condition based on roleId and searchTerm
            Expression<Func<Account, bool>> filter = a =>
                (!roleId.HasValue || a.RoleId == roleId.Value) &&
                (string.IsNullOrEmpty(searchTerm) || a.Staff.Firstname.Contains(searchTerm) || a.Staff.Lastname.Contains(searchTerm));

            // Define sorting logic (always ascending by StaffName)
            Func<IQueryable<Account>, IOrderedQueryable<Account>> orderBy = q => q.OrderBy(e => e.Staff.Firstname);

            // Fetch data with filtering and sorting
            var entities = await _unitOfWork.AccountRepository.GetAsync(
                filter,
                orderBy,
                pageIndex: pageIndex,
                pageSize: pageSize,
                includeProperties: "Staff,Role"
            );

            // Map the entities to the response DTO
            var response = _mapper.Map<List<ResponseAccount>>(entities);

            var result = new ResponseModel
            {
                Data = response,
                MessageError = ""
            };

            // Count the total elements based on the same filter
            result.TotalElements = await CountAsync(a =>
                (!roleId.HasValue || a.RoleId == roleId.Value) &&
                (string.IsNullOrEmpty(searchTerm) || a.Staff.Firstname.Contains(searchTerm))
            );

            // Calculate the total number of pages
            result.TotalPages = result.CalculateTotalPageCount(pageSize);

            // Return the response model
            return result;
        }
        catch (Exception ex)
        {
            return new ResponseModel
            {
                Data = null,
                MessageError = ex.Message
            };
        }
    }

}