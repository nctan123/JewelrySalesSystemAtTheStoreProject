using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Service.Models.PurchasePriceRatioModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PurchasePriceRatioController : ControllerBase
{
    private readonly IPurchasePriceRatioService _purchasePriceRatioService;

    public PurchasePriceRatioController(IPurchasePriceRatioService purchasePriceRatioService)
    {
        _purchasePriceRatioService = purchasePriceRatioService;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create([FromBody] RequestCreatePurchasePriceRatio requestCreatePurchasePriceRatio)
    {
        var responseModel = await _purchasePriceRatioService.CreateAsync(requestCreatePurchasePriceRatio);
        return Ok(responseModel);
    }

    [HttpGet]
    [Route("GetByReturnBuyBackPolicyId")]
    public async Task<IActionResult> GetByReturnBuyBackPolicyId(int id)
    {
        var response = await _purchasePriceRatioService.GetByReturnBuyBackPolicyIdAsync(id);
        return Ok(response);
    }
}