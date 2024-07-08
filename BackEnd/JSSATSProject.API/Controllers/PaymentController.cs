using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Service.Models.OrderModel;
using JSSATSProject.Service.Models.PaymentDetailModel;
using JSSATSProject.Service.Models.PaymentModel;
using JSSATSProject.Service.Models.SellOrderModel;
using JSSATSProject.Service.Models.SpecialDiscountRequestModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IGuaranteeService _guaranteeService;
    private readonly IPaymentDetailService _paymentDetailService;
    private readonly IPaymentService _paymentService;
    private readonly IPointService _pointService;
    private readonly ISellOrderDetailService _sellOrderDetailService;
    private readonly ISellOrderService _sellOrderService;
    private readonly ISpecialDiscountRequestService _specialDiscountRequestService;
    private readonly IVnPayService _vnPayService;

    public PaymentController(IPaymentService paymentService, IVnPayService vnPayService,
        IPaymentDetailService paymentDetailService, ISellOrderService sellOrderService,
        IGuaranteeService guaranteeService, ISellOrderDetailService sellOrderDetailService,
        IPointService pointService, ISpecialDiscountRequestService specialDiscountRequestService)
    {
        _paymentService = paymentService;
        _vnPayService = vnPayService;
        _paymentDetailService = paymentDetailService;
        _sellOrderService = sellOrderService;
        _guaranteeService = guaranteeService;
        _sellOrderDetailService = sellOrderDetailService;
        _pointService = pointService;
        _specialDiscountRequestService = specialDiscountRequestService;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAllAsync(int pageIndex, int pageSize)
    {
        var responseModel = await _paymentService.GetAllAsync(pageIndex, pageSize);
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
            var updatesellorderstatus = new UpdateSellOrderStatus()
            {
                Status = OrderConstants.CanceledStatus,
                //Description = "Cancelled Payment"
            };
            //update sellorder
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
            if (response.VnPayResponseCode != "00" || response.VnPayTranStatus != "00")
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
                    Status = OrderConstants.ProcessingStatus

                };
                await _sellOrderService.UpdateStatusAsync(Convert.ToInt32(response.OrderId), updatesellsrderstatus);
            }

            //Create Guarantee
            var products =
                await _sellOrderDetailService.GetProductFromSellOrderDetailAsync(Convert.ToInt32(response.OrderId));
            await _guaranteeService.CreateGuaranteeAsync(products);

            //Update Point
            var sellorder = await _sellOrderService.GetEntityByIdAsync(Convert.ToInt32(response.OrderId));
            var discountPoint = sellorder.DiscountPoint;
            var customerPhone = sellorder.Customer.Phone;
            var sellorderAmount = sellorder.TotalAmount;
            //await _pointService.DecreaseCustomerAvailablePointAsync(customerPhone, discountPoint);
            await _pointService.AddCustomerPoint(customerPhone, sellorderAmount);

            //Update SpecialDiscount
            var specialDiscount = sellorder.SpecialDiscountRequestId;
            if (specialDiscount != null)
            {
                var specialDiscountId = sellorder.SpecialDiscountRequest.RequestId;
                var newspecialdiscount = new UpdateSpecialDiscountRequest
                {
                    Status = "used"
                };
                await _specialDiscountRequestService.UpdateAsync(specialDiscountId, newspecialdiscount);
            }

            return Ok(response);
        }
        catch (Exception ex)
        {
            // Log the exception here
            return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
        }

    }

    [HttpGet]
    [Route("GetTotalAllPayMent")]
    public async Task<IActionResult> GetTotalAllPayMenAsync(DateTime startDate, DateTime endDate)
    {
        var totalCount = await _paymentService.GetTotalAllPayMentAsync(startDate, endDate);
            return Ok(totalCount);
    }

}