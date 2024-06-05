using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models.DiamondModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JSSATSProject.API.Controllers
{
    [ApiController]
    public class DiamondController : ControllerBase
    {
        private IDiamondService _diamondService;

        public DiamondController(IDiamondService diamondService)
        {
            _diamondService = diamondService;
        }

        [HttpGet]
        [Route("api/[controller]/GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _diamondService.GetAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("api/[controller]/GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseModel = await _diamondService.GetByIdAsync(id);
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("api/[controller]/GetByName")]
        public async Task<IActionResult> GetByNameAsync(string name)
        {
            var responseModel = await _diamondService.GetByNameAsync(name);
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("api/[controller]/GetByCode")]
        public async Task<IActionResult> GetByCodeAsync(string code)
        {
            var responseModel = await _diamondService.GetByCodeAsync(code);
            return Ok(responseModel);
        }

        [HttpPost]
        [Route("api/[controller]/CreateDiamond")]
        public async Task<IActionResult> CreateAsync(RequestCreateDiamond requestDiamond)
        {
            var responseModel = await _diamondService.CreateDiamondAsync(requestDiamond);
            return Ok(responseModel);
        }

        [HttpPut]
        [Route("api/[controller]/UpdateDiamond")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] RequestUpdateDiamond requestDiamond)
        {
            var response = await _diamondService.UpdateDiamondAsync(id, requestDiamond);
            return Ok(response);
        }
    }
}