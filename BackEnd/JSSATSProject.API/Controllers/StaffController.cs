using JSSATSProject.Service.Models.StaffModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JSSATSProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _staffService.GetAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("GetDetailsByDate")]
        public async Task<IActionResult> GetDetailsByDateAsync([FromRoute]int id,DateTime startdate, DateTime enddate)
        {
            var responseModel = await _staffService.GetDetailsByDateAsync(id,startdate,enddate);
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseModel = await _staffService.GetByIdAsync(id);
            return Ok(responseModel);
        }

        [HttpPost]
        [Route("CreateStaff")]
        public async Task<IActionResult> CreateAsync([FromBody] RequestCreateStaff requestStaff)
        {
            var responseModel = await _staffService.CreateStaffAsync(requestStaff);
            return Ok(responseModel);
        }

        [HttpPut]
        [Route("UpdateStaff")]
        public async Task<IActionResult> UpdateStaffAsync(int id, [FromBody] RequestUpdateStaff requestStaff)
        {
            var response = await _staffService.UpdateStaffAsync(id, requestStaff);
            return Ok(response);
        }
    }
}