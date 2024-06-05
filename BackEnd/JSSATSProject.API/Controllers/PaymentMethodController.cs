using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models.PaymentMethodModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JSSATSProject.API.Controllers
{
    public class PaymentMethodController : ControllerBase
    {
        private readonly IPaymentMethodService _paymentMethodService;

        public PaymentMethodController(IPaymentMethodService paymentMethodService)
        {
            _paymentMethodService = paymentMethodService;
        }

        [HttpGet]
        [Route("api/[controller]/GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _paymentMethodService.GetAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("api/[controller]/GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseModel = await _paymentMethodService.GetByIdAsync(id);
            return Ok(responseModel);
        }

        [HttpPost]
        [Route("api/[controller]/CreatePaymentMethod")]
        public async Task<IActionResult> CreateAsync([FromBody] RequestCreatePaymentMethod requestPaymentMethod)
        {
            var responseModel = await _paymentMethodService.CreatePaymentMethodAsync(requestPaymentMethod);
            return Ok(responseModel);
        }

        [HttpPut]
        [Route("api/[controller]/UpdatePaymentMethod")]
        public async Task<IActionResult> UpdatePaymentMethodAsync(int paymentmethodId, [FromBody] RequestUpdatePaymentMethod requestPaymentMethod)
        {
            var response = await _paymentMethodService.UpdatePaymentMethodAsync(paymentmethodId, requestPaymentMethod);
            return Ok(response);
        }
    }
}