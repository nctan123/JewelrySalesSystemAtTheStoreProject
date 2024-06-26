using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace JSSATSProject.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IPointService _pointService;

        public AccountController(IAccountService accountService, IPointService pointService)
        {
            _accountService = accountService;
            _pointService = pointService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync(int pageIndex = 1, int pageSize = 10)
        {
            var responseModel = await _accountService.GetAllAsync(pageIndex, pageSize);
            return Ok(responseModel);
        }
//-------------------------------------------------------------
        [HttpGet]
        [Route("AddPoint")]
        public async Task<IActionResult> Test()
        {
            var responseModel = await _pointService.AddCustomerPoint("1234567890", 1000);
            return Ok(responseModel);
        }
        
        [HttpGet]
        [Route("DecreasePoint")]
        public async Task<IActionResult> Test2()
        {
            var responseModel = await _pointService.DecreaseCustomerAvailablePoint("1234567890", 500);
            return Ok(responseModel);
        }
    }
}