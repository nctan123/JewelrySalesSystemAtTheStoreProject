using JSSATSProject.Service.Models.ProductCategoryModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JSSATSProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _productCategoryService.GetAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseModel = await _productCategoryService.GetByIdAsync(id);
            return Ok(responseModel);
        }

        [HttpPost]
        [Route("CreateProductCategory")]
        public async Task<IActionResult> CreateAsync([FromBody] RequestCreateProductCategory requestProductCategory)
        {
            var responseModel = await _productCategoryService.CreateProductCategoryAsync(requestProductCategory);
            return Ok(responseModel);
        }
    }
}