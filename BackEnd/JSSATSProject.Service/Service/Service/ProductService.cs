using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.ProductModel;
using JSSATSProject.Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSSATSProject.Service.Service.Service
{
    public class ProductService : IProductService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel> CreateProductAsync(RequestCreateProduct requestProduct)
        {
            var entity = _mapper.Map<Product>(requestProduct);
            await _unitOfWork.ProductRepository.InsertAsync(entity);
            await _unitOfWork.SaveAsync();
            return new ResponseModel
            {
                Data = entity,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetAllAsync()
        {
            var entities = await _unitOfWork.ProductRepository.GetAsync();
            var response = _mapper.Map<List<ResponseProduct>>(entities);
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetByCodeAsync(string code)
        {
            var response = await _unitOfWork.ProductRepository.GetAsync(
                    c => c.Code.Equals(code),
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
                    MessageError = $"Customer with name '{code}' not found.",
                };
            }

            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.ProductRepository.GetByIDAsync(id);
            var response = _mapper.Map<ResponseProduct>(entity);
            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetByNameAsync(string name)
        {
            var response = await _unitOfWork.ProductRepository.GetAsync(
                    c => c.Name.Equals(name),
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

        public async Task<ResponseModel> UpdateProductAsync(int productId, RequestUpdateProduct requestProduct)
        {
            try
            {
                var product = await _unitOfWork.ProductRepository.GetByIDAsync(productId);
                if (product != null)
                {

                    _mapper.Map(requestProduct, product);

                    await _unitOfWork.ProductRepository.UpdateAsync(product);

                    return new ResponseModel
                    {
                        Data = product,
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