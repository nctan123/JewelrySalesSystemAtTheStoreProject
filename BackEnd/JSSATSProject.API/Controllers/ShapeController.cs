using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers
{
    //[Authorize(Roles = RoleConstants.Manager)]
    [ApiController]
    [Route("api/[controller]")]
    public class ShapeController : ControllerBase
    {
            private readonly IShapeService _shapeService;

            public ShapeController(IShapeService shapeService)
            {
                _shapeService = shapeService;
            }

            [HttpGet]
            [Route("GetAll")]
            public async Task<IActionResult> GetAllAsync()
            {
                var responseModel = await _shapeService.GetAllAsync();
                return Ok(responseModel);
            }
        }
}
