using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Service.Models.Material;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers;

[Authorize(Roles = RoleConstants.Manager)]
[Route("api/[controller]")]
[ApiController]
public class MaterialController : ControllerBase
{
    private readonly IMaterialService _materialService;

    public MaterialController(IMaterialService materialService)
    {
        _materialService = materialService;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAllAsync()
    {
        var responseModel = await _materialService.GetAllAsync();
        return Ok(responseModel);
    }

    [HttpGet]
    [Route("GetById")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var responseModel = await _materialService.GetByIdAsync(id);
        return Ok(responseModel);
    }

    [HttpPost]
    [Route("CreateMaterial")]
    public async Task<IActionResult> CreateAsync([FromBody] RequestCreateMaterial requestMaterial)
    {
        var responseModel = await _materialService.CreateMaterialAsync(requestMaterial);
        return Ok(responseModel);
    }
}