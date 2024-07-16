using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Service.Models.StaffModel;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers;

[Authorize(Roles = RoleConstants.Manager)]
[Route("api/[controller]")]
[ApiController]
public class StaffController : ControllerBase
{
    private readonly IStaffService _staffService;

    public StaffController(IStaffService staffService)
    {
        _staffService = staffService;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate,
        [FromQuery] int pageIndex, [FromQuery] int pageSize, [FromQuery] string sortBy,
        [FromQuery] bool ascending = true)
    {
        try
        {
            var responseModel =
                await _staffService.GetAllSellerAsync(startDate, endDate, pageIndex, pageSize, sortBy, ascending);
            return Ok(responseModel);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }


    [HttpGet("Search")]
    public async Task<IActionResult> SearchAsync([FromQuery] string nameSearch, [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate, [FromQuery] int pageIndex, [FromQuery] int pageSize)
    {
        var responseModel = await _staffService.SearchSellerAsync(nameSearch, startDate, endDate, pageIndex, pageSize);
        return Ok(responseModel);
    }


    [HttpPost]
    [Route("CreateStaff")]
    public async Task<IActionResult> CreateAsync([FromBody] RequestCreateStaff requestStaff)
    {
        var responseModel = await _staffService.CreateStaffAsync(requestStaff);
        return Ok(responseModel);
    }

    [HttpPut]
    [Route("UpdateStaff")]
    public async Task<IActionResult> UpdateStaffAsync(int id, [FromBody] RequestUpdateStaff requestStaff)
    {
        var response = await _staffService.UpdateStaffAsync(id, requestStaff);
        return Ok(response);
    }

    [HttpGet]
    [Route("GetTop6ByMonth")]
    public async Task<IActionResult> GetTop6Async(DateTime startDate, DateTime endDate)
    {
        var responseModel = await _staffService.GetTop6ByDateAsync(startDate, endDate);
        return Ok(responseModel);
    }

    [HttpGet]
    [Route("GetStaffSymmary")]
    public async Task<IActionResult> GetStaffSymmary([FromQuery] int id, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var response = await _staffService.GetStaffSymmaryAsync(id, startDate, endDate);
        if (response.Data == null)
        {
            return NotFound(response.MessageError);
        }
        return Ok(response);
    }

    [HttpGet]
    [Route("GetSellOrderByStaffId")]
    public async Task<IActionResult> GetSellOrdersByStaffIdAsync(int id, int pageIndex, int pageSize, DateTime startDate, DateTime endDate)
    {
        var responseModel = await _staffService.GetSellOrdersByStaffIdAsync(id, pageIndex, pageSize,startDate,endDate);
        return Ok(responseModel);
    }

    [HttpGet]
    [Route("GetBuyOrdersStaffId")]
    public async Task<IActionResult> GetBuyOrdersStaffIdAsync(int id, int pageIndex, int pageSize, DateTime startDate, DateTime endDate)
    {
        var responseModel = await _staffService.GetBuyOrdersByStaffIdAsync(id, pageIndex, pageSize, startDate, endDate);
        return Ok(responseModel);
    }

    [HttpGet]
    [Route("SearchSellOrders")]
    public async Task<IActionResult> SearchSellOrders(int id, string orderCode, int pageIndex, int pageSize, DateTime startDate, DateTime endDate)
    {
        var result = await _staffService.SearchSellOrdersByStaffIdAsync(id, orderCode, pageIndex, pageSize, startDate, endDate);
        return Ok(result);
    }

    [HttpGet]
    [Route("SearchBuyOrders")]
    public async Task<IActionResult> SearchBuyOrders(int id, string orderCode, int pageIndex , int pageSize , DateTime startDate, DateTime endDate)
    {
        var result = await _staffService.SearchBuyOrdersByStaffIdAsync(id, orderCode, pageIndex, pageSize, startDate, endDate);
        return Ok(result);
    }

    [HttpGet]
    [Route("GetById")]
    public async Task<IActionResult> GetByIdOrders(int id)
    {
        var result = await _staffService.GetByIdAsync(id);
        return Ok(result);
    }
}