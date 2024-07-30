using JSSATSProject.Service.Models.DiamondModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DiamondController : ControllerBase
{
    private readonly IDiamondService _diamondService;

    public DiamondController(IDiamondService diamondService)
    {
        _diamondService = diamondService;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAllAsync()
    {
        var responseModel = await _diamondService.GetAllAsync();
        return Ok(responseModel);
    }

    [HttpGet]
    [Route("GetById")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var responseModel = await _diamondService.GetByIdAsync(id);
        return Ok(responseModel);
    }

    [HttpGet]
    [Route("GetByName")]
    public async Task<IActionResult> GetByNameAsync(string name)
    {
        var responseModel = await _diamondService.GetByNameAsync(name);
        return Ok(responseModel);
    }

    [HttpGet]
    [Route("GetByCode")]
    public async Task<IActionResult> GetByCodeAsync(string code)
    {
        var responseModel = await _diamondService.GetByCodeAsync(code);
        return Ok(responseModel);
    }

    [HttpPost]
    [Route("CreateDiamond")]
    public async Task<IActionResult> CreateAsync(RequestCreateDiamond requestDiamond)
    {
        var responseModel = await _diamondService.CreateDiamondAsync(requestDiamond);
        return Ok(responseModel);
    }

    [HttpPut]
    [Route("UpdateDiamond")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] RequestUpdateDiamond requestDiamond)
    {
        var response = await _diamondService.UpdateDiamondAsync(id, requestDiamond);
        return Ok(response);
    }

    [HttpPut]
    [Route("UpdateStatusDiamond")]
    public async Task<IActionResult> UpdateStatusAsync(int id, [FromBody] RequestUpdateStatusDiamond requestDiamond)
    {
        var response = await _diamondService.UpdateStatusDiamondAsync(id, requestDiamond);
        return Ok(response);
    }
}