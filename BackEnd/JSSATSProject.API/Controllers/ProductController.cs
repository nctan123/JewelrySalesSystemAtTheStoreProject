using JSSATSProject.Service.Models.ProductModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JSSATSProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _productService.GetAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("GetByCode")]
        public async Task<IActionResult> GetByCodeAsync(string code)
        {
            var responseModel = await _productService.GetByCodeAsync(code);
            return Ok(responseModel);
        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateAsync([FromBody] RequestCreateProduct requestProduct)
        {
            var responseModel = await _productService.CreateProductAsync(requestProduct);
            return Ok(responseModel);
        }

        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProductAsync(int id, [FromBody] RequestUpdateProduct requestProduct)
        {
            var response = await _productService.UpdateProductAsync(id, requestProduct);
            return Ok(response);
        }

        [HttpGet("GetAll01")]
        public async Task<IActionResult> GetAllAsync([FromQuery] int categoryId,[FromQuery] int pageIndex = 1,[FromQuery] int pageSize = 10,[FromQuery] bool ascending = true)
        {
            try
            {
                var responseModel = await _productService.GetAllAsync(categoryId, pageIndex, pageSize, ascending);
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search(int categoryId, string searchTerm, int pageIndex = 1, int pageSize = 10)
        {

            var responseModel = await _productService.SearchProductsAsync(categoryId, searchTerm, pageIndex, pageSize);
            return Ok(responseModel);

        }
    }
}