using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.ProductModel;
using JSSATSProject.Service.Service.IService;


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
            var entities = await _unitOfWork.ProductRepository.GetAsync(
                c => c.Code.Equals(code),
                includeProperties: "Category,Stalls");


            var response = entities.Select(product => new ResponseProductDetails
            {
                Id = product.Id,
                CategoryName = product.Category.Name,
                StallName = product.Stalls.Name,
                Code = product.Code,
                Name = product.Name,
                MaterialCost = product.MaterialCost,
                ProductionCost = product.ProductionCost,
                GemCost = product.GemCost,
                Img = product.Img,
                PriceRate = product.PriceRate,
                Status = product.Status
            }).ToList();

            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetByIdAsync(int id)
        {
            var entities = await _unitOfWork.ProductRepository.GetAsync(
                c => c.Id.Equals(id),
                includeProperties: "Category,Stalls");


            var response = entities.Select(product => new ResponseProductDetails
            {
                Id = product.Id,
                CategoryName = product.Category.Name,
                StallName = product.Stalls.Name,
                Code = product.Code,
                Name = product.Name,
                MaterialCost = product.MaterialCost,
                ProductionCost = product.ProductionCost,
                GemCost = product.GemCost,
                Img = product.Img,
                PriceRate = product.PriceRate,
                Status = product.Status
            }).ToList();

            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetByNameAsync(string name)
        {
            var entities = await _unitOfWork.ProductRepository.GetAsync(
               c => c.Name.Equals(name),
               includeProperties: "Category,Stalls");


            var response = entities.Select(product => new ResponseProductDetails
            {
                Id = product.Id,
                CategoryName = product.Category.Name,
                StallName = product.Stalls.Name,
                Code = product.Code,
                Name = product.Name,
                MaterialCost = product.MaterialCost,
                ProductionCost = product.ProductionCost,
                GemCost = product.GemCost,
                Img = product.Img,
                PriceRate = product.PriceRate,
                Status = product.Status
            }).ToList();

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

        public async Task<ResponseModel> UpdateStatusProductAsync(int productId, RequestUpdateStatusProduct requestProduct)
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