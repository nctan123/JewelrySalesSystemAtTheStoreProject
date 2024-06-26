using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models.PurchasePriceRatioModel;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchasePriceRatioController : ControllerBase
    {
        IPurchasePriceRatioService _purchasePriceRatioService;

        public PurchasePriceRatioController(PurchasePriceRatioService purchasePriceRatioService)
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
    }
}
