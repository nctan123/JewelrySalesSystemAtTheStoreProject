using JSSATSProject.Service.Models.OrderDetail;
using JSSATSProject.Service.Models.OrderModel;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderdetailService;

        public OrderDetailController(IOrderDetailService orderdetailService)
        {
            _orderdetailService = orderdetailService;
        }

        [HttpGet]
        [Route("GetByOrderId")]
        public async Task<IActionResult> GetByOrderIdAsync(int id)
        {
            var responseModel = await _orderdetailService.GetByOrderIdAsync(id);
            return Ok(responseModel);
        }

        [HttpPost]
        [Route("CreateOrderDetail")]
        public async Task<IActionResult> CreateAsync([FromBody] RequestCreateOrderDetail requestOrderDetail)
        {
            var responseModel = await _orderdetailService.CreateOrderDetailAsync(requestOrderDetail);
            return Ok(responseModel);
        }

        [HttpPut]
        [Route("UpdateOrderDetail")]
        public async Task<IActionResult> UpdateOrderDetailAsync(int id, [FromBody] RequestUpdateOrderDetail requestOrderDetail)
        {
            var response = await _orderdetailService.UpdateOrderDetailAsync(id, requestOrderDetail);
            return Ok(response);
        }
    }
}
