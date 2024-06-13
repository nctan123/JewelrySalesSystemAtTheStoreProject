using JSSATSProject.Service.Models.DiamondModel;
using JSSATSProject.Service.Models.PromotionRequestModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromotionRequestController : ControllerBase
    {

        private IPromotionRequestService  _promotionrequestService;
        public PromotionRequestController(IPromotionRequestService promotionrequestService)
        {
            _promotionrequestService = promotionrequestService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _promotionrequestService.GetAllAsync();
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
        public async Task<IActionResult> UpdatePromotionRequestAsync(int id, [FromBody] UpdatePromotionRequest promotionRequest)
        {
            var response = await _promotionrequestService.UpdatePromotionRequestAsync(id, promotionRequest);
            return Ok(response);
        }
    }
}
