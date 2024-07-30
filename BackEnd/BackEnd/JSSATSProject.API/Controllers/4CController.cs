using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace JSSATSProject.API.Controllers
{
    [Authorize(Roles = RoleConstants.Manager)]
    [ApiController]
    [Route("api/[controller]")]
    public class _4CController : ControllerBase
    {
        private readonly I4CService _4CService;

        public _4CController(I4CService I4CService)
        {
            _4CService = I4CService;
        }

        [HttpGet]
        [Route("GetCaratAll")]
        public async Task<IActionResult> GetCaratAllAsync()
        {
            var responseModel = await _4CService.GetCaratAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("GetCutAll")]
        public async Task<IActionResult> GetCutAllAsync()
        {
            var responseModel = await _4CService.GetCutAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("GetColorAll")]
        public async Task<IActionResult> GetColorAllAsync()
        {
            var responseModel = await _4CService.GetColorAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("GetClarityAll")]
        public async Task<IActionResult> GetClarityAllAsync()
        {
            var responseModel = await _4CService.GetClarityAllAsync();
            return Ok(responseModel);
        }
    }
}
