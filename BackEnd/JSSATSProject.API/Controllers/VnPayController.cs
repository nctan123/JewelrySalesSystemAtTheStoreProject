using JSSATSProject.Service.Models.PaymentModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VnPayController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;

        public VnPayController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

  
        [HttpPost("CreatePaymentUrl")]
        public IActionResult CreatePaymentUrl([FromBody] RequestCreatePayment model)
        {
            if (model == null)
            {
                return BadRequest("Invalid payment request model.");
            }

            try
            {
                var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
                // Response.Redirect(url);
                return Redirect(url);
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}
