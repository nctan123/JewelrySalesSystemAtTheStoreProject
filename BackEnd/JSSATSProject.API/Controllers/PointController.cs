using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models.PointModel;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JSSATSProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointController : ControllerBase
    {
        private readonly IPointService _pointService;

        public PointController(IPointService pointService)
        {
            _pointService = pointService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _pointService.GetAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseModel = await _pointService.GetByIdAsync(id);
            return Ok(responseModel);
        }

        [HttpPost]
        [Route("CreatePoint")]
        public async Task<IActionResult> CreateAsync([FromBody] RequestCreatePoint requestPoint)
        {
            var responseModel = await _pointService.CreatePointAsync(requestPoint);
            return Ok(responseModel);
        }

        [HttpPut]
        [Route("UpdatePoint")]
        public async Task<IActionResult> UpdatePointAsync(int pointId, [FromBody] RequestUpdatePoint requestPoint)
        {
            var response = await _pointService.UpdatePointAsync(pointId, requestPoint);
            return Ok(response);
        }
    }
}