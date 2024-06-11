using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace JSSATSProject.API.Controllers
{
<<<<<<< HEAD
    // [Authorize]
=======
    //[Authorize]
>>>>>>> ef1d898c610203bb40990ce34f1644abc601b704
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _accountService.GetAllAsync();
            return Ok(responseModel);
        }
    }
}