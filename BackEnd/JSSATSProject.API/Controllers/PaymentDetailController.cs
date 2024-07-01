using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Service.Models.OrderModel;
using JSSATSProject.Service.Models.PaymentDetailModel;
using JSSATSProject.Service.Models.PaymentModel;
using JSSATSProject.Service.Models.SpecialDiscountRequestModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentDetailController : ControllerBase
{
    private readonly IGuaranteeService _guaranteeService;
    private readonly IPaymentDetailService _paymentDetailService;
    private readonly IPaymentService _paymentService;
    private readonly IPointService _pointService;
    private readonly ISellOrderDetailService _sellOrderDetailService;
    private readonly ISellOrderService _sellOrderService;
    private readonly ISpecialDiscountRequestService _specialDiscountRequestService;
    private readonly IVnPayService _vnPayService;

    public PaymentDetailController(IPaymentDetailService paymentDetailService, IPaymentService paymentService,
        ISellOrderService sellOrderService, IVnPayService vnPayService, IGuaranteeService guaranteeService,
        ISellOrderDetailService sellOrderDetailService, IPointService pointService,
        ISpecialDiscountRequestService specialDiscountRequestService)
    {
        _paymentDetailService = paymentDetailService;
        _paymentService = paymentService;
        _sellOrderService = sellOrderService;
        _vnPayService = vnPayService;
        _sellOrderDetailService = sellOrderDetailService;
        _guaranteeService = guaranteeService;
        _pointService = pointService;
        _specialDiscountRequestService = specialDiscountRequestService;
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
            Status = OrderConstants.ProcessingStatus
     
        };
        var sellorderId = await _paymentService.GetOrderIdByPaymentIdAsync(requestPaymentDetail.PaymentId);
        await _sellOrderService.UpdateStatusAsync(sellorderId, updatesellsrderstatus);

        //Create Guarantee
        var products = await _sellOrderDetailService.GetProductFromSellOrderDetailAsync(sellorderId);
        await _guaranteeService.CreateGuaranteeAsync(products);

        //Update Point
        var sellorder = await _sellOrderService.GetEntityByIdAsync(sellorderId);
        var discountPoint = sellorder.DiscountPoint;
        var customerPhone = sellorder.Customer.Phone;
        var sellorderAmount = sellorder.TotalAmount;
        await _pointService.DecreaseCustomerAvailablePointAsync(customerPhone, discountPoint);
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

        return Ok();
    }
}