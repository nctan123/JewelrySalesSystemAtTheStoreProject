using Azure;
using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Service.Models.OrderModel;
using JSSATSProject.Service.Models.PaymentDetailModel;
using JSSATSProject.Service.Models.PaymentModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailController : ControllerBase
    {
        private readonly IPaymentDetailService _paymentDetailService;
        private readonly IPaymentService _paymentService;
        private readonly IVnPayService _vnPayService;
        private readonly ISellOrderService _sellOrderService;
        private readonly IGuaranteeService _guaranteeService;
        private readonly ISellOrderDetailService _sellOrderDetailService;

        public PaymentDetailController(IPaymentDetailService paymentDetailService, IPaymentService paymentService,
               ISellOrderService sellOrderService, IVnPayService vnPayService,IGuaranteeService guaranteeService,
               ISellOrderDetailService sellOrderDetailService
               )
        {
            _paymentDetailService = paymentDetailService;
            _paymentService = paymentService;
            _sellOrderService = sellOrderService;
            _vnPayService = vnPayService;
            _sellOrderDetailService = sellOrderDetailService;
            _guaranteeService = guaranteeService;
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
            // Create Payment
            var responseModel = await _paymentDetailService.CreatePaymentDetailAsync(requestPaymentDetail);

            //Update Status Payment
            var payment = new RequestUpdatePayment
            {
                Status = "completed"
            };
            await _paymentService.UpdatePaymentAsync(requestPaymentDetail.PaymentId, payment);


           // Update Status SellOrder
            var updatesellsrderstatus = new UpdateSellOrderStatus
            {
                Status = OrderConstants.ProcessingStatus,
                Description = ""
            };
            var orderId = await _paymentService.GetOrderIdByPaymentIdAsync(requestPaymentDetail.PaymentId);
            await _sellOrderService.UpdateStatusAsync(orderId, updatesellsrderstatus);

            //Create Guarantee
            var products = await _sellOrderDetailService.GetProductFromSellOrderDetailAsync(orderId);
            await _guaranteeService.CreateGuaranteeAsync(products);


            return Ok();
        }
    }
}
