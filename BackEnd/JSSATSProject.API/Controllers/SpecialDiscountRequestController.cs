using JSSATSProject.Service.Models.PromotionRequestModel;
using JSSATSProject.Service.Models.SpecialDiscountRequestModel;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecialDiscountRequestController : ControllerBase
    {
        private ISpecialDiscountRequestService _specialdiscountrequestService;
        public SpecialDiscountRequestController(ISpecialDiscountRequestService specialdiscountrequestService)
        {
            _specialdiscountrequestService = specialdiscountrequestService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _specialdiscountrequestService.GetAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("GetByCustomerId")]
        public async Task<IActionResult> GetByCustomerIdAsync(int id)
        {
            var responseModel = await _specialdiscountrequestService.GetByCustomerIdAsync(id);
            return Ok(responseModel);
        }


        [HttpPost]
        [Route("CreateSpecialDiscountRequest")]
        public async Task<IActionResult> CreateSpecialDiscountRequestAsync(CreateSpecialDiscountRequest specialdiscountRequest)
        {
            var responseModel = await _specialdiscountrequestService.CreateSpecialDiscountRequestAsync(specialdiscountRequest);
            return Ok(responseModel);
        }

        [HttpPut]
        [Route("UpdateSpecialDiscountRequest")]
        public async Task<IActionResult> UpdateSpecialDiscountRequestAsync(int id, [FromBody] UpdateSpecialDiscountRequest specialdiscountRequest)
        {
            var response = await _specialdiscountrequestService.UpdateSpecialDiscountRequestAsync(id, specialdiscountRequest);
            return Ok(response);
        }
    }
}
