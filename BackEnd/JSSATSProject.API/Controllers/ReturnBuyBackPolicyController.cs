using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models.ReturnBuyBackPolicyModel;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JSSATSProject.API.Controllers
{
    public class ReturnBuyBackPolicyController : ControllerBase
    {
        private readonly IReturnBuyBackPolicyService _returnBuyBackPolicyService;

        public ReturnBuyBackPolicyController(IReturnBuyBackPolicyService returnBuyBackPolicyService)
        {
            _returnBuyBackPolicyService = returnBuyBackPolicyService;
        }

        [HttpGet]
        [Route("api/[controller]/GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _returnBuyBackPolicyService.GetAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("api/[controller]/GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseModel = await _returnBuyBackPolicyService.GetByIdAsync(id);
            return Ok(responseModel);
        }

        [HttpPost]
        [Route("api/[controller]/CreateReturnBuyBackPolicy")]
        public async Task<IActionResult> CreateAsync([FromBody] RequestCreateReturnBuyBackPolicy requestReturnBuyBackPolicy)
        {
            var responseModel = await _returnBuyBackPolicyService.CreateReturnBuyBackPolicyAsync(requestReturnBuyBackPolicy);
            return Ok(responseModel);
        }

        [HttpPut]
        [Route("api/[controller]/UpdateReturnBuyBackPolicy")]
        public async Task<IActionResult> UpdateReturnBuyBackPolicyAsync(int Id, [FromBody] RequestUpdateReturnBuyBackPolicy requestReturnBuyBackPolicy)
        {
            var response = await _returnBuyBackPolicyService.UpdateReturnBuyBackPolicyAsync(Id, requestReturnBuyBackPolicy);
            return Ok(response);
        }
    }
}