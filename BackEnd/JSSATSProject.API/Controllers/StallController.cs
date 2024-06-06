using JSSATSProject.Service.Models.StallModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JSSATSProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StallController : ControllerBase
    {
        private readonly IStallService _stallService;

        public StallController(IStallService stallService)
        {
            _stallService = stallService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _stallService.GetAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseModel = await _stallService.GetByIdAsync(id);
            return Ok(responseModel);
        }

        [HttpPost]
        [Route("CreateStall")]
        public async Task<IActionResult> CreateAsync([FromBody] RequestCreateStall requestStall)
        {
            var responseModel = await _stallService.CreateStallAsync(requestStall);
            return Ok(responseModel);
        }
    }
}