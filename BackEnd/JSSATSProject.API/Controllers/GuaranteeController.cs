using JSSATSProject.Service.Models.GuaranteeModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;


namespace JSSATSProject.API.Controllers
{
    public class GuaranteeController : ControllerBase
    {
        private readonly IGuaranteeService _guaranteeService;

        public GuaranteeController(IGuaranteeService guaranteeService)
        {
            _guaranteeService = guaranteeService;
        }

        [HttpGet]
        [Route("api/[controller]/GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _guaranteeService.GetAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("api/[controller]/GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseModel = await _guaranteeService.GetByIdAsync(id);
            return Ok(responseModel);
        }

        [HttpPost]
        [Route("api/[controller]/CreateGuarantee")]
        public async Task<IActionResult> CreateAsync([FromBody] RequestCreateGuarantee requestGuarantee)
        {
            var responseModel = await _guaranteeService.CreateGuaranteeAsync(requestGuarantee);
            return Ok(responseModel);
        }

        [HttpPut]
        [Route("api/[controller]/UpdateGuarantee")]
        public async Task<IActionResult> UpdateAsync(int guaranteeId, [FromBody] RequestUpdateGuarantee requestGuarantee)
        {
            var responseModel = await _guaranteeService.UpdateGuaranteeAsync(guaranteeId, requestGuarantee);
            return Ok(responseModel);
        }
    }
}