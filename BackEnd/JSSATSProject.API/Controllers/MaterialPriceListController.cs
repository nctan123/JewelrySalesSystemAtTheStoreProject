using JSSATSProject.Service.Models.MaterialPriceListModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MaterialPriceListController : ControllerBase
{
    private readonly IMaterialPriceListService _materialPriceListService;

    public MaterialPriceListController(IMaterialPriceListService materialPriceListService)
    {
        _materialPriceListService = materialPriceListService;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAllAsync([FromQuery] DateTime? effectiveDate, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var responseModel = await _materialPriceListService.GetAllAsync(effectiveDate, page, pageSize);
        return Ok(responseModel);
    }


    [HttpGet]
    [Route("GetById")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var responseModel = await _materialPriceListService.GetByIdAsync(id);
        return Ok(responseModel);
    }

    [HttpPost]
    [Route("CreateMaterialPriceList")]
    public async Task<IActionResult> CreateAsync([FromBody] RequestCreateMaterialPriceList requestMaterialPriceList)
    {
        var responseModel = await _materialPriceListService.CreateMaterialPriceListAsync(requestMaterialPriceList);
        return Ok(responseModel);
    }

    [HttpPut]
    [Route("UpdateMaterialPriceList")]
    public async Task<IActionResult> UpdateMaterialPriceListAsync(int Id,
        [FromBody] RequestUpdateMaterialPriceList requestMaterialPriceList)
    {
        var response = await _materialPriceListService.UpdateMaterialPriceListAsync(Id, requestMaterialPriceList);
        return Ok(response);
    }
}