using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models.PaymentMethodModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JSSATSProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IPaymentMethodService _paymentMethodService;

        public PaymentMethodController(IPaymentMethodService paymentMethodService)
        {
            _paymentMethodService = paymentMethodService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _paymentMethodService.GetAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseModel = await _paymentMethodService.GetByIdAsync(id);
            return Ok(responseModel);
        }

        [HttpPost]
        [Route("CreatePaymentMethod")]
        public async Task<IActionResult> CreateAsync([FromBody] RequestCreatePaymentMethod requestPaymentMethod)
        {
            var responseModel = await _paymentMethodService.CreatePaymentMethodAsync(requestPaymentMethod);
            return Ok(responseModel);
        }

        [HttpPut]
        [Route("UpdatePaymentMethod")]
        public async Task<IActionResult> UpdatePaymentMethodAsync(int paymentmethodId, [FromBody] RequestUpdatePaymentMethod requestPaymentMethod)
        {
            var response = await _paymentMethodService.UpdatePaymentMethodAsync(paymentmethodId, requestPaymentMethod);
            return Ok(response);
        }
    }
}