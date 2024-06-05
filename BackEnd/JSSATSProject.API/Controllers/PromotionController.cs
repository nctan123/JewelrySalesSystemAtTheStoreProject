using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models.PromotionModel;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JSSATSProject.API.Controllers
{
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;

        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        [HttpGet]
        [Route("api/[controller]/GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _promotionService.GetAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("api/[controller]/GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseModel = await _promotionService.GetByIdAsync(id);
            return Ok(responseModel);
        }

        [HttpPost]
        [Route("api/[controller]/CreatePromotion")]
        public async Task<IActionResult> CreateAsync([FromBody] RequestCreatePromotion requestPromotion)
        {
            var responseModel = await _promotionService.CreatePromotionAsync(requestPromotion);
            return Ok(responseModel);
        }

        [HttpPut]
        [Route("api/[controller]/UpdatePromotion")]
        public async Task<IActionResult> UpdatePromotionAsync(int Id, [FromBody] RequestUpdatePromotion requestPromotion)
        {
            var response = await _promotionService.UpdatePromotionAsync(Id, requestPromotion);
            return Ok(response);
        }
    }
}