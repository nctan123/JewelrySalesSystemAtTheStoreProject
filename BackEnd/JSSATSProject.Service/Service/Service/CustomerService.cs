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
        
            var entities = await _unitOfWork.CustomerRepository.GetAsync(includeProperties: "Point,Orders,Payments");
            var response = entities.Select(entity => new ResponseCustomer
            {
                Id = entity.Id,
                PointId = entity.PointId,
                Firstname = entity.Firstname,
                Lastname = entity.Lastname,
                Phone = entity.Phone,
                Email = entity.Email,
                Gender = entity.Gender,
                Address = entity.Address,
                Orders = entity.Orders, 
                Payments = entity.Payments,
                TotalPoint = entity.Point?.Totalpoint?? 0,
                AvaliablePoint = entity.Point?.AvailablePoint ?? 0
            }).ToList();

            // Return the mapped response
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }


        public async Task<ResponseModel> GetByIdAsync(int id)
        {
            var entities = await _unitOfWork.CustomerRepository.GetAsync(
                c => c.Id.Equals(id),
                includeProperties: "Point,Orders,Payments");
            var response = entities.Select(entity => new ResponseCustomer
            {
                Id = entity.Id,
                PointId = entity.PointId,
                Firstname = entity.Firstname,
                Lastname = entity.Lastname,
                Phone = entity.Phone,
                Email = entity.Email,
                Gender = entity.Gender,
                Address = entity.Address,
                Orders = entity.Orders,
                Payments = entity.Payments,
                TotalPoint = entity.Point?.Totalpoint ?? 0,
                AvaliablePoint = entity.Point?.AvailablePoint ?? 0
            }).ToList();

            // Return the mapped response
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
            var entities = await _unitOfWork.CustomerRepository.GetAsync(
                c => c.Firstname.Equals(name),
                includeProperties: "Point,Orders,Payments");
            var response = entities.Select(entity => new ResponseCustomer
            {
                Id = entity.Id,
                PointId = entity.PointId,
                Firstname = entity.Firstname,
                Lastname = entity.Lastname,
                Phone = entity.Phone,
                Email = entity.Email,
                Gender = entity.Gender,
                Address = entity.Address,
                Orders = entity.Orders,
                Payments = entity.Payments,
                TotalPoint = entity.Point?.Totalpoint ?? 0,
                AvaliablePoint = entity.Point?.AvailablePoint ?? 0
            }).ToList();

            // Return the mapped response
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetByPhoneAsync(string phonenumber)
        {
            var entities = await _unitOfWork.CustomerRepository.GetAsync(
                 c => c.Phone.Equals(phonenumber),
                 includeProperties: "Point,Orders,Payments");
            var response = entities.Select(entity => new ResponseCustomer
            {
                Id = entity.Id,
                PointId = entity.PointId,
                Firstname = entity.Firstname,
                Lastname = entity.Lastname,
                Phone = entity.Phone,
                Email = entity.Email,
                Gender = entity.Gender,
                Address = entity.Address,
                Orders = entity.Orders,
                Payments = entity.Payments,
                TotalPoint = entity.Point?.Totalpoint ?? 0,
                AvaliablePoint = entity.Point?.AvailablePoint ?? 0
            }).ToList();

            // Return the mapped response
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
                var customer = await _unitOfWork.CustomerRepository.GetByIDAsync(customerId);
                if (customer != null)
                {
                    int id = customer.Id;
                    _mapper.Map(requestCustomer, customer);
                    customer.Id = id;
                    await _unitOfWork.CustomerRepository.UpdateAsync(customer);

                    return new ResponseModel
                    {
                        Data = customer,
                        MessageError = "",
                    };
                }

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