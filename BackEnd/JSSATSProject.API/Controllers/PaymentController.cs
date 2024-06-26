using Azure;
using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models.OrderModel;
using JSSATSProject.Service.Models.PaymentDetailModel;
using JSSATSProject.Service.Models.PaymentModel;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Drawing.Printing;
using System.Threading.Tasks;

namespace JSSATSProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IVnPayService _vnPayService;
        private readonly IPaymentDetailService _paymentDetailService;
        private readonly ISellOrderService _sellOrderService;
        private readonly IGuaranteeService _guaranteeService;
        private readonly ISellOrderDetailService _sellOrderDetailService;

        public PaymentController(IPaymentService paymentService, IVnPayService vnPayService,
            IPaymentDetailService paymentDetailService, ISellOrderService sellOrderService,
            IGuaranteeService guaranteeService, ISellOrderDetailService sellOrderDetailService
            )
        {
            _paymentService = paymentService;
            _vnPayService = vnPayService;
            _paymentDetailService = paymentDetailService;
            _sellOrderService = sellOrderService;
            _guaranteeService = guaranteeService;
            _sellOrderDetailService = sellOrderDetailService;
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

            if (requestPayment.Status.Equals("cancelled"))
            {
                var updatesellorderstatus = new UpdateSellOrderStatus
                {
                    Status = OrderConstants.CanceledStatus,
                    Description = "Cancelled Payment"
                };
                var orderId = await _paymentService.GetOrderIdByPaymentIdAsync(id);

                await _sellOrderService.UpdateStatusAsync(orderId, updatesellorderstatus);
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("callback")]
        public async Task<IActionResult> PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            // VnPay
            //Create PaymenDetail
            //Update Status Payment
            //Update Status SellOrdwer
            try
            { 
                //Status_PaymentDetail == failed 
                if (Convert.ToInt32(response.TransactionId) == 0)
                {

                    var order = await _sellOrderService.GetEntityByIdAsync(Convert.ToInt32(response.OrderId));
                    var paymentDetail = new RequestCreatePaymentDetail
                    {
                        PaymentId = Convert.ToInt32(response.PaymentId),
                        PaymentMethodId = Convert.ToInt32(response.PaymentMethodId),
                        Amount = order.TotalAmount,
                        ExternalTransactionCode = response.TransactionId,
                        Status = "failed"
                    };
                    await _paymentDetailService.CreatePaymentDetailAsync(paymentDetail);


                }
                //Status_PaymentDetail == completed
                else
                {
                    var order = await _sellOrderService.GetEntityByIdAsync(Convert.ToInt32(response.OrderId));
                    var paymentDetail = new RequestCreatePaymentDetail
                    {
                        PaymentId = Convert.ToInt32(response.PaymentId),
                        PaymentMethodId = Convert.ToInt32(response.PaymentMethodId),
                        Amount = order.TotalAmount,
                        ExternalTransactionCode = response.TransactionId,
                        Status = "completed"
                    };
                    await _paymentDetailService.CreatePaymentDetailAsync(paymentDetail);

                    //Update Status Payment
                    var payment = new RequestUpdatePayment
                    {
                        Status = "completed"
                    };
                    await _paymentService.UpdatePaymentAsync(Convert.ToInt32(response.PaymentId), payment);
                    //Update Status SellOrder
                    var updatesellsrderstatus = new UpdateSellOrderStatus
                    {
                        Status = OrderConstants.ProcessingStatus,
                        Description = ""
                    };
                    await _sellOrderService.UpdateStatusAsync(Convert.ToInt32(response.OrderId), updatesellsrderstatus);


                }

                //Create Guarantee
                var products = await _sellOrderDetailService.GetProductFromSellOrderDetailAsync(Convert.ToInt32(response.OrderId));
                await _guaranteeService.CreateGuaranteeAsync(products);

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