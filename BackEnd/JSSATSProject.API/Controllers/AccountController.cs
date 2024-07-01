using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers;

//[Authorize]
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
    public async Task<IActionResult> GetAllAsync(int pageIndex = 1, int pageSize = 10, int? roleId = null)
    {
        var responseModel = await _accountService.GetAllAsync(pageIndex, pageSize, roleId);
        return Ok(responseModel);
    }
}