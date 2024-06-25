using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Service.IService;
using System.Linq.Expressions;


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


            var point = new Point
            {
                AvailablePoint = 0,
                Totalpoint = 0
            };

            await _unitOfWork.PointRepository.InsertAsync(point);
            await _unitOfWork.SaveAsync();

            entity.PointId = point.Id;
            entity.Point = point;


            await _unitOfWork.CustomerRepository.InsertAsync(entity);
            await _unitOfWork.SaveAsync();

            return new ResponseModel
            {
                Data = entity,
                MessageError = "",
            };
        }


        public async Task<ResponseModel> GetAllAsync(int pageIndex, int pageSize)
        {
            var entities = await _unitOfWork.CustomerRepository.GetAsync(
                orderBy: query => query.OrderByDescending(c => c.CreateDate).ThenBy(c => c.Firstname),
                includeProperties: "Point",
                pageIndex: pageIndex,
                pageSize: pageSize
               
            );

            var response = entities.Select(entity => _mapper.Map<ResponseCustomer>(entity)).ToList();

            // Return the mapped response
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> SearchAsync(string searchTerm, int pageIndex = 1, int pageSize = 10)
        {
            Expression<Func<Customer, bool>> filter = customer =>
                customer.Firstname.Contains(searchTerm) ||
                customer.Lastname.Contains(searchTerm) ||
                customer.Phone.Contains(searchTerm);

            var customers = await _unitOfWork.CustomerRepository.GetAsync(
                filter: filter,
                orderBy: query => query.OrderByDescending(c => c.CreateDate).ThenBy(c => c.Firstname),
                includeProperties: "Point",
                pageIndex: pageIndex,
                pageSize: pageSize
            );

            var response = customers.Select(customer => _mapper.Map<ResponseCustomer>(customer)).ToList();

            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetByPhoneAsync(string phoneNumber)
        {
            var entities = await _unitOfWork.CustomerRepository.GetAsync(
                c => c.Phone.Equals(phoneNumber),
                includeProperties: "Point");
            var response = entities.Select(entity => new ResponseCustomer
            {
                Id = entity.Id,
                Firstname = entity.Firstname,
                Lastname = entity.Lastname,
                Phone = entity.Phone,
                Email = entity.Email,
                Gender = entity.Gender,
                Address = entity.Address,
            }).ToList();

            // Return the mapped response
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetEntityByPhoneAsync(string phoneNumber)
        {
            var entity = await _unitOfWork.CustomerRepository.GetAsync(
                    c => c.Phone.Equals(phoneNumber),
                    includeProperties: "Point,SellOrders,Payments,SpecialDiscountRequests");

            // Return the mapped response
            return new ResponseModel
            {
                Data = entity.FirstOrDefault(),
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

        public async Task<ResponseModel> CountNewCustomer(DateTime startDate, DateTime endDate)
        {
            var customers = await _unitOfWork.CustomerRepository.GetAsync(
                filter: c => c.CreateDate >= startDate && c.CreateDate <= endDate
            );

            var count = customers.Count();

            return new ResponseModel
            {
                MessageError = "",
                Data = count
            };
        }
    }
}