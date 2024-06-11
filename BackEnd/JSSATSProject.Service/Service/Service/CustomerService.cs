using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JSSATSProject.Repository.ConstantsContainer;

namespace JSSATSProject.Service.Service.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel> CreateCustomerAsync(RequestCreateCustomer requestCustomer)
        {
            var entity = _mapper.Map<Customer>(requestCustomer);
            await _unitOfWork.CustomerRepository.InsertAsync(entity);
            await _unitOfWork.SaveAsync();
            return new ResponseModel
            {
                Data = entity,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetAllAsync()
        {
            var entities = await _unitOfWork.CustomerRepository.GetAsync();
            var response = _mapper.Map<List<ResponseCustomer>>(entities.ToList());
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.CustomerRepository.GetByIDAsync(id);
            var response = _mapper.Map<ResponseCustomer>(entity);
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> FindByPhoneNumber(string phoneNumberStr)
        {
            if (!int.TryParse(phoneNumberStr, out int phoneNumber))
                return new ResponseModel
                {
                    Data = null,
                    MessageError = Constants.InvalidPhoneNumberFormat
                };

            var customer = await _unitOfWork.CustomerRepository.FindByPhoneNumber(phoneNumberStr);

            return new ResponseModel
            {
                Data = customer,
                MessageError = customer is null ? Constants.CustomerNotFound : "",
            };
        }

        public async Task<ResponseModel> GetByNameAsync(string name)
        {
            var response = await _unitOfWork.CustomerRepository.GetAsync(
                c => c.Firstname.Equals(name),
                null,
                includeProperties: "",
                pageIndex: null,
                pageSize: null
            );

            if (!response.Any())
            {
                return new ResponseModel
                {
                    Data = null,
                    MessageError = $"Customer with name '{name}' not found.",
                };
            }

            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetByPhoneAsync(string phonenumber)
        {
            var response = await _unitOfWork.CustomerRepository.GetAsync(
                c => c.Phone.Equals(phonenumber),
                null,
                includeProperties: "",
                pageIndex: null,
                pageSize: null
            );

            if (!response.Any())
            {
                return new ResponseModel
                {
                    Data = null,
                    MessageError = $"Customer with name '{phonenumber}' not found.",
                };
            }

            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> UpdateCustomerAsync(int customerId, RequestUpdateCustomer requestCustomer)
        {
            try
            {
                var customer = _mapper.Map<Customer>(requestCustomer);
                customer.Id = customerId;
                await _unitOfWork.CustomerRepository.UpdateAsync(customer);
                // await _unitOfWork.SaveAsync();

                return new ResponseModel
                {
                    Data = customer,
                    MessageError = "",
                };
                return new ResponseModel
                {
                    Data = null,
                    MessageError = "Not Found",
                };
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate error response
                return new ResponseModel
                {
                    Data = null,
                    MessageError = "An error occurred while updating the customer: " + ex.Message
                };
            }
        }
    }
}