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
        private readonly IVnPayService _vnPayService;

        public PaymentController(IPaymentService paymentService, IVnPayService vnPayService)
        {
            _paymentService = paymentService;
            _vnPayService = vnPayService;
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
        
        [HttpGet]
        [Route("callback")]
        public IActionResult PaymentCallback()
        {
            try
            {
                var response = _vnPayService.PaymentExecute(Request.Query);
                //create payment details
                if (Convert.ToInt32(response.TransactionId) == 0)
                {
                    return Problem();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}