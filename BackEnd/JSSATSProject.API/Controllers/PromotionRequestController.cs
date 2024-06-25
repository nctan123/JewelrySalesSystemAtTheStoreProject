using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.DiamondModel;
using JSSATSProject.Service.Models.PromotionRequestModel;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
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
        public async Task<IActionResult> GetAllAsync(int pageIndex, bool ascending )
        {
            try
            {
                var responseModel = await _promotionrequestService.GetAllAsync(pageIndex, 10, ascending);
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel
                {
                    Data = null,
                    MessageError = $"An error occurred: {ex.Message}"
                });
            }
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

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> SearchAsync(string description)
        {
            
                var responseModel = await _promotionrequestService.SearchAsync(description);
                return Ok(responseModel);
            
           
        }
    }
}
