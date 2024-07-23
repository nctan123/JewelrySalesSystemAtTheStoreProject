using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Service.Models.BuyOrderModel;
using JSSATSProject.Service.Models.OrderModel;
using JSSATSProject.Service.Models.PaymentDetailModel;
using JSSATSProject.Service.Models.PaymentModel;
using JSSATSProject.Service.Models.SellOrderModel;
using JSSATSProject.Service.Models.SpecialDiscountRequestModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers;

[Authorize]
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
    private readonly IBuyOrderService _buyOrderService;

    private readonly IHttpClientFactory _httpClientFactory;

    public PaymentController(IPaymentService paymentService, IVnPayService vnPayService,
        IPaymentDetailService paymentDetailService, ISellOrderService sellOrderService,
        IGuaranteeService guaranteeService, ISellOrderDetailService sellOrderDetailService,
        IPointService pointService, ISpecialDiscountRequestService specialDiscountRequestService,
        IBuyOrderService buyOrderService, IHttpClientFactory httpClientFactory)
    {
        _paymentService = paymentService;
        _vnPayService = vnPayService;
        _paymentDetailService = paymentDetailService;
        _sellOrderService = sellOrderService;
        _guaranteeService = guaranteeService;
        _sellOrderDetailService = sellOrderDetailService;
        _pointService = pointService;
        _specialDiscountRequestService = specialDiscountRequestService;
        _buyOrderService = buyOrderService;
        _httpClientFactory = httpClientFactory;
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
            };

            var updatebuyorderstatus = new RequestUpdateBuyOrderStatus() { 
                NewStatus = OrderConstants.CanceledStatus
            };

            
            //update Order
            var sellorderId = await _paymentService.GetSellOrderIdByPaymentIdAsync(id);
            var buyorderId = await _paymentService.GetBuyOrderIdByPaymentIdAsync(id); 

            if(sellorderId != null)
            {
                //sellorder
                await _sellOrderService.UpdateStatusAsync(sellorderId.Value, updatesellorderstatus);
            }
            else
            {
                //buyorder
                await _buyOrderService.UpdateAsync(buyorderId.Value, updatebuyorderstatus);
            }

            
        }

        return Ok(response);
    }

    //[HttpGet]
    //[Route("callback")]
    //public async Task PaymentCallback()
    //{
    //    var response = _vnPayService.PaymentExecute(Request.Query);

    //    var order = await _sellOrderService.GetEntityByIdAsync(Convert.ToInt32(response.OrderId));
    //    var paymentDetail = new RequestCreatePaymentDetail
    //    {
    //        PaymentId = Convert.ToInt32(response.PaymentId),
    //        PaymentMethodId = Convert.ToInt32(response.PaymentMethodId),
    //        Amount = order.TotalAmount,
    //        ExternalTransactionCode = response.TransactionId,
    //        Status = (response.VnPayResponseCode != "00" || response.VnPayTranStatus != "00") ? "failed" : "completed"
    //    };

    //    await _paymentDetailService.CreatePaymentDetailAsync(paymentDetail);

    //    if (response.VnPayResponseCode == "00" && response.VnPayTranStatus == "00")
    //    {
    //        // Update Status Payment
    //        var payment = new RequestUpdatePayment
    //        {
    //            Status = "completed"
    //        };
    //        await _paymentService.UpdatePaymentAsync(Convert.ToInt32(response.PaymentId), payment);

    //        // Update Status SellOrder
    //        var updatesellorderstatus = new UpdateSellOrderStatus
    //        {
    //            Status = OrderConstants.ProcessingStatus
    //        };
    //        await _sellOrderService.UpdateStatusAsync(Convert.ToInt32(response.OrderId), updatesellorderstatus);

    //        // Create Guarantee
    //        var products = await _sellOrderDetailService.GetProductFromSellOrderDetailAsync(Convert.ToInt32(response.OrderId));
    //        await _guaranteeService.CreateGuaranteeAsync(products);

    //        // Update Point
    //        var sellorder = await _sellOrderService.GetEntityByIdAsync(Convert.ToInt32(response.OrderId));
    //        var discountPoint = sellorder.DiscountPoint;
    //        var customerPhone = sellorder.Customer.Phone;
    //        var sellorderAmount = await _sellOrderService.GetFinalPriceAsync(sellorder);
    //        await _pointService.AddCustomerPoint(customerPhone, sellorderAmount);

    //        // Update SpecialDiscount
    //        var specialDiscount = sellorder.SpecialDiscountRequestId;
    //        if (specialDiscount != null)
    //        {
    //            var specialDiscountId = sellorder.SpecialDiscountRequest.RequestId;
    //            var newspecialdiscount = new UpdateSpecialDiscountRequest
    //            {
    //                Status = "used"
    //            };
    //            await _specialDiscountRequestService.UpdateAsync(specialDiscountId, newspecialdiscount);
    //        }
    //    }
    //}


    [HttpGet]
    [Route("GetTotalAllPayMent")]
    public async Task<IActionResult> GetTotalAllPayMentAsync(DateTime startDate, DateTime endDate, int order)
    {
        var totalCount = await _paymentService.GetTotalAllPayMentAsync(startDate, endDate, order);
            return Ok(totalCount);
    }
}