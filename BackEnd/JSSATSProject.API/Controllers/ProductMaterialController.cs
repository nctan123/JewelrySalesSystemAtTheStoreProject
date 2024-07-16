using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models.ProductMaterialModel;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers
{
    [Authorize(Roles =RoleConstants.Manager)]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductMaterialController : ControllerBase
    {
        private readonly IProductMaterialService _productMaterialService;

        public ProductMaterialController(IProductMaterialService productMaterialService)
        {
            _productMaterialService = productMaterialService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] RequestCreateProductMaterial requestProductMaterial)
        {
            var responseModel = await _productMaterialService.CreateAsync(requestProductMaterial);
            return Ok(responseModel);
        }

    }
}
