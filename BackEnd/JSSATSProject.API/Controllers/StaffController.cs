using JSSATSProject.Service.Models.StaffModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers;

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
                await _staffService.GetAllAsync(startDate, endDate, pageIndex, pageSize, sortBy, ascending);
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
        var responseModel = await _staffService.SearchAsync(nameSearch, startDate, endDate, pageIndex, pageSize);
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
    [Route("GetById")]
    public async Task<IActionResult> GetById([FromQuery] int id, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var response = await _staffService.GetByIdAsync(id, startDate, endDate);
        if (response.Data == null)
        {
            return NotFound(response.MessageError);
        }
        return Ok(response);
    }
}