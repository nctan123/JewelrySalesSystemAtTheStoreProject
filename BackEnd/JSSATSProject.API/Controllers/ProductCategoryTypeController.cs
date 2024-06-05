using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JSSATSProject.API.Controllers
{
    public class ProductCategoryTypeController : ControllerBase
    {
        private readonly IProductCategoryTypeService _productCategoryTypeService;

        public ProductCategoryTypeController(IProductCategoryTypeService productCategoryTypeService)
        {
            _productCategoryTypeService = productCategoryTypeService;
        }

        [HttpGet]
        [Route("api/[controller]/GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _productCategoryTypeService.GetAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("api/[controller]/GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseModel = await _productCategoryTypeService.GetByIdAsync(id);
            return Ok(responseModel);
        }
    }
}