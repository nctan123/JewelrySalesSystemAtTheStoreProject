using Azure;
using JSSATSProject.Service.Models.PaymentDetailModel;
using JSSATSProject.Service.Models.PaymentModel;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailController : ControllerBase
    {
        private readonly IPaymentDetailService _paymentDetailService;
        private readonly IPaymentService _paymentService;
        public PaymentDetailController(IPaymentDetailService paymentDetailService,IPaymentService paymentService)
        {
            _paymentDetailService = paymentDetailService;
            _paymentService = paymentService;
        }

        [HttpGet]
        [Route("GetByPaymentId")]
        public async Task<IActionResult> GetByPaymentIdAsync(int id)
        {
            var responseModel = await _paymentDetailService.GetByPaymentIdAsync(id);
            return Ok(responseModel);
        }

        [HttpPost]
        [Route("CreatePaymentDetail")]
        public async Task<IActionResult> CreateAsync([FromBody] RequestCreatePaymentDetail requestPaymentDetail)
        {
            // Cash
            //Được tạo sau khi chọn phương thức Cash + ấn nút hoàn tất
            //DF_Status_PaymentDetail == Completed
            var responseModel = await _paymentDetailService.CreatePaymentDetailAsync(requestPaymentDetail);

            //Update Status Payment
            var payment = new RequestUpdatePayment
            {
                Status = "completed"
            };
            await _paymentService.UpdatePaymentAsync(requestPaymentDetail.PaymentId, payment);

            return Ok(responseModel);
        }
    }
}
