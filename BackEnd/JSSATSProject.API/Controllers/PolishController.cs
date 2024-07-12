using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PolishController : ControllerBase
    {
        private readonly IPolishService _polishService;

        public PolishController(IPolishService polishService)
        {
            _polishService = polishService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _polishService.GetAllAsync();
            return Ok(responseModel);
        }
    }
}
