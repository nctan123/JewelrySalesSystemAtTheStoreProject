using JSSATSProject.Service.Models.StaffModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JSSATSProject.API.Controllers
{
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet]
        [Route("api/[controller]/GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _staffService.GetAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("api/[controller]/GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseModel = await _staffService.GetByIdAsync(id);
            return Ok(responseModel);
        }

        [HttpPost]
        [Route("api/[controller]/CreateStaff")]
        public async Task<IActionResult> CreateAsync([FromBody] RequestCreateStaff requestStaff)
        {
            var responseModel = await _staffService.CreateStaffAsync(requestStaff);
            return Ok(responseModel);
        }

        [HttpPut]
        [Route("api/[controller]/UpdateStaff")]
        public async Task<IActionResult> UpdateStaffAsync(int id, [FromBody] RequestUpdateStaff requestStaff)
        {
            var response = await _staffService.UpdateStaffAsync(id, requestStaff);
            return Ok(response);
        }
    }
}