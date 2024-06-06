using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models.OrderModel;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JSSATSProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _orderService.GetAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseModel = await _orderService.GetByIdAsync(id);
            return Ok(responseModel);
        }

        [HttpPost]
        [Route("CreateOrder")]
        public async Task<IActionResult> CreateAsync([FromBody] RequestCreateOrder requestOrder)
        {
            var responseModel = await _orderService.CreateOrderAsync(requestOrder);
            return Ok(responseModel);
        }

        [HttpPut]
        [Route("UpdateOrder")]
        public async Task<IActionResult> UpdateOrderAsync(int id, [FromBody] RequestUpdateOrder requestOrder)
        {
            var response = await _orderService.UpdateOrderAsync(id, requestOrder);
            return Ok(response);
        }
    }
}