using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models.OrderModel;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JSSATSProject.API.Controllers
{
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("api/[controller]/GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _orderService.GetAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("api/[controller]/GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseModel = await _orderService.GetByIdAsync(id);
            return Ok(responseModel);
        }

        [HttpPost]
        [Route("api/[controller]/CreateOrder")]
        public async Task<IActionResult> CreateAsync([FromBody] RequestCreateOrder requestOrder)
        {
            var responseModel = await _orderService.CreateOrderAsync(requestOrder);
            return Ok(responseModel);
        }

        [HttpPut]
        [Route("api/[controller]/UpdateOrder")]
        public async Task<IActionResult> UpdateOrderAsync(int id, [FromBody] RequestUpdateOrder requestOrder)
        {
            var response = await _orderService.UpdateOrderAsync(id, requestOrder);
            return Ok(response);
        }
    }
}