using JSSATSProject.Service.Models.PromotionModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JSSATSProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;

        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _promotionService.GetAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseModel = await _promotionService.GetByIdAsync(id);
            return Ok(responseModel);
        }

        [HttpPost]
        [Route("CreatePromotion")]
        public async Task<IActionResult> CreateAsync([FromBody] RequestCreatePromotion requestPromotion)
        {
            var responseModel = await _promotionService.CreatePromotionAsync(requestPromotion);
            return Ok(responseModel);
        }

        [HttpPut]
        [Route("UpdatePromotion")]
        public async Task<IActionResult> UpdatePromotionAsync(int Id, [FromBody] RequestUpdatePromotion requestPromotion)
        {
            var response = await _promotionService.UpdatePromotionAsync(Id, requestPromotion);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetPromotionByProductCategory")]
        public async Task<IActionResult> GetPromotionByProductCategoryAsync(int productCategoryId)
        {
            var responseModel = await _promotionService.GetPromotionByProductCategoryAsync(productCategoryId);
            return Ok(responseModel);
        }
    }
}