using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models.OrderModel;
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
        private readonly ISpecialDiscountRequestService _specialdiscountrequestService;
        private readonly ISellOrderService _sellOrderService;

        public SpecialDiscountRequestController(ISpecialDiscountRequestService specialdiscountrequestService,
            ISellOrderService sellOrderService)
        {
            _specialdiscountrequestService = specialdiscountrequestService;
            _sellOrderService = sellOrderService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync(bool ascending = true, int pageIndex = 1, int pageSize = 10)
        {
            var responseModel =
                await _specialdiscountrequestService.GetAllAsync(ascending, pageIndex, pageSize);
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> GetAsync(string orderCode)
        {
            var responseModel =
                await _specialdiscountrequestService.GetAsync(orderCode);
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
        public async Task<IActionResult> CreateSpecialDiscountRequestAsync(
            CreateSpecialDiscountRequest specialdiscountRequest)
        {
            var responseModel = await _specialdiscountrequestService.CreateAsync(specialdiscountRequest);
            return Ok(responseModel);
        }

        [HttpPut]
        [Route("UpdateSpecialDiscountRequest/{id}")]
        public async Task<IActionResult> UpdateSpecialDiscountRequestAsync(int id,
            [FromBody] UpdateSpecialDiscountRequest specialdiscountRequest)
        {
            var response = await _specialdiscountrequestService.UpdateAsync(id, specialdiscountRequest);
            var targetEntity = await _specialdiscountrequestService.GetEntityByIdAsync(id);
            var sellOrderId = targetEntity!.SellOrders.First().Id;
            await _sellOrderService.UpdateOrderAsync(sellOrderId, new RequestUpdateSellOrder
            {
                SpecialDiscountRequest = targetEntity,
                Status = OrderConstants.DraftStatus
            });

            return Ok(response);
        }
    }
}