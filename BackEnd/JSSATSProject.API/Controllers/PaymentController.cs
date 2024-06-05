using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models.PaymentModel;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JSSATSProject.API.Controllers
{
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        [Route("api/[controller]/GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _paymentService.GetAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("api/[controller]/GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseModel = await _paymentService.GetByIdAsync(id);
            return Ok(responseModel);
        }

        [HttpPost]
        [Route("api/[controller]/CreatePayment")]
        public async Task<IActionResult> CreateAsync([FromBody] RequestCreatePayment requestPayment)
        {
            var responseModel = await _paymentService.CreatePaymentAsync(requestPayment);
            return Ok(responseModel);
        }

        [HttpPut]
        [Route("api/[controller]/UpdatePayment")]
        public async Task<IActionResult> UpdatePaymentAsync(int id, [FromBody] RequestUpdatePayment requestPayment)
        {
            var response = await _paymentService.UpdatePaymentAsync(id, requestPayment);
            return Ok(response);
        }
    }
}