using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProductCategoryTypeController : ControllerBase
{
    private readonly IProductCategoryTypeService _productCategoryTypeService;

    public ProductCategoryTypeController(IProductCategoryTypeService productCategoryTypeService)
    {
        _productCategoryTypeService = productCategoryTypeService;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAllAsync()
    {
        var responseModel = await _productCategoryTypeService.GetAllAsync();
        return Ok(responseModel);
    }

    [HttpGet]
    [Route("GetById")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var responseModel = await _productCategoryTypeService.GetByIdAsync(id);
        return Ok(responseModel);
    }
}