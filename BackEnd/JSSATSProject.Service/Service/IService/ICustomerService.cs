﻿using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.CustomerModel;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.IService
{
    public interface ICustomerService
    {
        public Task<ResponseModel> GetAllAsync();
        public Task<ResponseModel> GetByIdAsync(int id);
        public Task<ResponseModel> GetByNameAsync(string name);
        public Task<ResponseModel> GetByPhoneAsync(string phoneNumber);
        public Task<ResponseModel> GetEntityByPhoneAsync(string phoneNumber);
        public Task<ResponseModel> CreateCustomerAsync(RequestCreateCustomer requestCustomer);
        public Task<ResponseModel> UpdateCustomerAsync(int customerId, RequestUpdateCustomer requestCustomer);
        public Task<ResponseModel> CountNewCustomer(DateTime startDate, DateTime endDate);
    }
}