using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Service.Models.BuyOrderModel;
using JSSATSProject.Service.Models.OrderModel;
using JSSATSProject.Service.Models.PaymentDetailModel;
using JSSATSProject.Service.Models.PaymentModel;
using JSSATSProject.Service.Models.SellOrderModel;
using JSSATSProject.Service.Models.SpecialDiscountRequestModel;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;

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
    private readonly IBuyOrderService _buyOrderService;

    public PaymentDetailController(IPaymentDetailService paymentDetailService, IPaymentService paymentService,
        ISellOrderService sellOrderService, IVnPayService vnPayService, IGuaranteeService guaranteeService,
        ISellOrderDetailService sellOrderDetailService, IPointService pointService,
        ISpecialDiscountRequestService specialDiscountRequestService, 
        IBuyOrderService buyOrderService)
    {
        _paymentDetailService = paymentDetailService;
        _paymentService = paymentService;
        _sellOrderService = sellOrderService;
        _vnPayService = vnPayService;
        _sellOrderDetailService = sellOrderDetailService;
        _guaranteeService = guaranteeService;
        _pointService = pointService;
        _specialDiscountRequestService = specialDiscountRequestService;
        _buyOrderService = buyOrderService;
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

        var sellorderId = await _paymentService.GetSellOrderIdByPaymentIdAsync(requestPaymentDetail.PaymentId);
        var buyorderId = await _paymentService.GetBuyOrderIdByPaymentIdAsync(requestPaymentDetail.PaymentId);

        if (sellorderId != null)
        {
            //sellorder
            var updatesellorderstatus = new UpdateSellOrderStatus()
            {
                Status = OrderConstants.ProcessingStatus
            };

            await _sellOrderService.UpdateStatusAsync(sellorderId.Value, updatesellorderstatus);

            //Create Guarantee
            var products = await _sellOrderDetailService.GetProductFromSellOrderDetailAsync(sellorderId.Value);
            await _guaranteeService.CreateGuaranteeAsync(products);

            //Update Point
            var sellorder = await _sellOrderService.GetEntityByIdAsync(sellorderId.Value);
            var discountPoint = sellorder.DiscountPoint;
            var customerPhone = sellorder.Customer.Phone;
            var sellorderAmount = await _sellOrderService.GetFinalPriceAsync(sellorder);
            //await _pointService.DecreaseCustomerAvailablePointAsync(customerPhone, discountPoint);
            await _pointService.AddCustomerPoint(customerPhone, sellorderAmount);

            //Update SpecialDiscount
            var specialDiscount = sellorder.SpecialDiscountRequestId;

            if (specialDiscount != null)
            {
                var specialDiscountId = sellorder.SpecialDiscountRequest.RequestId;
                var newspecialdiscount = new UpdateSpecialDiscountRequest
                {
                    DiscountRate = sellorder.SpecialDiscountRequest?.DiscountRate ?? 0,
                    Status = "used",
                    ApprovedBy = sellorder.SpecialDiscountRequest.ApprovedBy ?? 0
                };
                await _specialDiscountRequestService.UpdateAsync(specialDiscountId, newspecialdiscount);
            }
        }
        else
        {
            //buyorder
            var updatebuyorderstatus = new RequestUpdateBuyOrderStatus()
            {
                NewStatus = OrderConstants.CompletedStatus
            };

            await _buyOrderService.UpdateAsync(buyorderId.Value, updatebuyorderstatus);
        }


        return Ok();
    }
}