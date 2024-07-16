using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers
{
    [Authorize(Roles = RoleConstants.Manager)]
    [ApiController]
    [Route("api/[controller]")]
    public class OriginController : ControllerBase
    {

        private readonly IOriginService _originService;

        public OriginController(IOriginService originService)
        {
            _originService = originService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _originService.GetAllAsync();
            return Ok(responseModel);
        }
    }
}
