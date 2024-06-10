using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models.GuaranteeModel;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JSSATSProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class GuaranteeController : ControllerBase
    {
        private readonly IGuaranteeService _guaranteeService;

        public GuaranteeController(IGuaranteeService guaranteeService)
        {
            _guaranteeService = guaranteeService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _guaranteeService.GetAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseModel = await _guaranteeService.GetByIdAsync(id);
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("GetByProductId")]
        public async Task<IActionResult> GetByProductIdAsync(int productId)
        {
            var responseModel = await _guaranteeService.GetByProductIdAsync(productId);
            return Ok(responseModel);
        }

        [HttpPost]
        [Route("CreateGuarantee")]
        public async Task<IActionResult> CreateAsync([FromBody] RequestCreateGuarantee requestGuarantee)
        {
            var responseModel = await _guaranteeService.CreateGuaranteeAsync(requestGuarantee);
            return Ok(responseModel);
        }

        [HttpPut]
        [Route("UpdateGuarantee")]
        public async Task<IActionResult> UpdateAsync(int guaranteeId, [FromBody] RequestUpdateGuarantee requestGuarantee)
        {
            var responseModel = await _guaranteeService.UpdateGuaranteeAsync(guaranteeId, requestGuarantee);
            return Ok(responseModel);
        }
    }
}