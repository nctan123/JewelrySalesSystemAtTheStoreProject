using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Service.Models.PromotionRequestModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers;
[Authorize(Roles = RoleConstants.Manager + "," + RoleConstants.Admin)]
[ApiController]
[Route("api/[controller]")]
public class PromotionRequestController : ControllerBase
{
    private readonly IPromotionRequestService _promotionrequestService;

    public PromotionRequestController(IPromotionRequestService promotionrequestService)
    {
        _promotionrequestService = promotionrequestService;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAllAsync(int pageIndex, int pageSize, bool ascending)
    {
        var responseModel = await _promotionrequestService.GetAllAsync(pageIndex, pageSize, ascending);
        return Ok(responseModel);
    }

    [HttpPost]
    [Route("CreatePromotionRequest")]
    public async Task<IActionResult> CreatePromotionRequestAsync(CreatePromotionRequest promotionRequest)
    {
        var responseModel = await _promotionrequestService.CreatePromotionRequestAsync(promotionRequest);
        return Ok(responseModel);
    }

    [HttpPut]
    [Route("UpdatePromotionRequest")]
    public async Task<IActionResult> UpdatePromotionRequestAsync(int id,
        [FromBody] UpdatePromotionRequest promotionRequest)
    {
        var response = await _promotionrequestService.UpdatePromotionRequestAsync(id, promotionRequest);
        return Ok(response);
    }

    [HttpGet]
    [Route("Search")]
    public async Task<IActionResult> SearchAsync(string description, int pageIndex = 1, int pageSize = 10)
    {
        var responseModel = await _promotionrequestService.SearchAsync(description, pageIndex, pageSize);
        return Ok(responseModel);
    }

    [HttpGet]
    [Route("GetById")]
    public async Task<IActionResult> GetAllByIdAsync(int id)
    {
        var responseModel = await _promotionrequestService.GetByIdAsync(id);
        return Ok(responseModel);
    }

}