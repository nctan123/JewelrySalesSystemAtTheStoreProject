using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models.ProductModel;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JSSATSProject.API.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("api/[controller]/GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _productService.GetAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("api/[controller]/GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseModel = await _productService.GetByIdAsync(id);
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("api/[controller]/GetByName")]
        public async Task<IActionResult> GetByNameAsync(string name)
        {
            var responseModel = await _productService.GetByNameAsync(name);
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("api/[controller]/GetByCode")]
        public async Task<IActionResult> GetByCodeAsync(string code)
        {
            var responseModel = await _productService.GetByCodeAsync(code);
            return Ok(responseModel);
        }

        [HttpPost]
        [Route("api/[controller]/CreateProduct")]
        public async Task<IActionResult> CreateAsync([FromBody] RequestCreateProduct requestProduct)
        {
            var responseModel = await _productService.CreateProductAsync(requestProduct);
            return Ok(responseModel);
        }

        [HttpPut]
        [Route("api/[controller]/UpdateProduct")]
        public async Task<IActionResult> UpdateProductAsync(int id, [FromBody] RequestUpdateProduct requestProduct)
        {
            var response = await _productService.UpdateProductAsync(id, requestProduct);
            return Ok(response);
        }
    }
}