using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Service.Models.ProductDiamondModel;
using JSSATSProject.Service.Models.ProductMaterialModel;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers
{
    //[Authorize(Roles = RoleConstants.Manager)]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductDiamondController : ControllerBase
    {
        private readonly IProductDiamondService _productDiamondService;

        public ProductDiamondController(IProductDiamondService productDiamondService)
        {
            _productDiamondService = productDiamondService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] RequestCreateProductDiamond requestProductDiamond)
        {
            var responseModel = await _productDiamondService.CreateAsync(requestProductDiamond);
            return Ok(responseModel);
        }
    }
}
