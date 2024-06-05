using JSSATSProject.Service.Models.StallModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JSSATSProject.API.Controllers
{
    public class StallController : ControllerBase
    {
        private readonly IStallService _stallService;

        public StallController(IStallService stallService)
        {
            _stallService = stallService;
        }

        [HttpGet]
        [Route("api/[controller]/GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _stallService.GetAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("api/[controller]/GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseModel = await _stallService.GetByIdAsync(id);
            return Ok(responseModel);
        }

        [HttpPost]
        [Route("api/[controller]/CreateStall")]
        public async Task<IActionResult> CreateAsync([FromBody] RequestCreateStall requestStall)
        {
            var responseModel = await _stallService.CreateStallAsync(requestStall);
            return Ok(responseModel);
        }
    }
}