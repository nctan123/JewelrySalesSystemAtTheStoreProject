using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models.PaymentModel;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JSSATSProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _paymentService.GetAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseModel = await _paymentService.GetByIdAsync(id);
            return Ok(responseModel);
        }

        [HttpPost]
        [Route("CreatePayment")]
        public async Task<IActionResult> CreateAsync([FromBody] RequestCreatePayment requestPayment)
        {
            var responseModel = await _paymentService.CreatePaymentAsync(requestPayment);
            return Ok(responseModel);
        }

        [HttpPut]
        [Route("UpdatePayment")]
        public async Task<IActionResult> UpdatePaymentAsync(int id, [FromBody] RequestUpdatePayment requestPayment)
        {
            var response = await _paymentService.UpdatePaymentAsync(id, requestPayment);
            return Ok(response);
        }
    }
}