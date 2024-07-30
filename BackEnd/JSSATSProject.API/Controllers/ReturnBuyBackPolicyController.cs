using JSSATSProject.Service.Models.ReturnBuyBackPolicyModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ReturnBuyBackPolicyController : ControllerBase
{
    private readonly IReturnBuyBackPolicyService _returnBuyBackPolicyService;

    public ReturnBuyBackPolicyController(IReturnBuyBackPolicyService returnBuyBackPolicyService)
    {
        _returnBuyBackPolicyService = returnBuyBackPolicyService;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAllAsync(int pageIndex, bool ascending)
    {
        var responseModel = await _returnBuyBackPolicyService.GetAllAsync(pageIndex, 10, ascending);
        return Ok(responseModel);
    }

    [HttpPost]
    [Route("CreateReturnBuyBackPolicy")]
    public async Task<IActionResult> CreateAsync([FromBody] RequestCreateReturnBuyBackPolicy requestReturnBuyBackPolicy)
    {
        var responseModel =
            await _returnBuyBackPolicyService.CreateReturnBuyBackPolicyAsync(requestReturnBuyBackPolicy);
        return Ok(responseModel);
    }

    [HttpPut]
    [Route("UpdateReturnBuyBackPolicy")]
    public async Task<IActionResult> UpdateReturnBuyBackPolicyAsync(int Id,
        [FromBody] RequestUpdateReturnBuyBackPolicy requestReturnBuyBackPolicy)
    {
        var response = await _returnBuyBackPolicyService.UpdateReturnBuyBackPolicyAsync(Id, requestReturnBuyBackPolicy);
        return Ok(response);
    }

    [HttpGet]
    [Route("GetDisplayPolicy")]
    public async Task<IActionResult> GetDisplayPolicyAsync()
    {
        var responseModel = await _returnBuyBackPolicyService.GetDisplayPolicy();
        return Ok(responseModel);
    }
}