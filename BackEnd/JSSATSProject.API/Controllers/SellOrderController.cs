using AutoMapper;
using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.OrderModel;
using JSSATSProject.Service.Models.SellOrderModel;
using JSSATSProject.Service.Models.SpecialDiscountRequestModel;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SellOrderController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPointService _pointService;
    private readonly ISellOrderService _sellOrderService;
    private readonly ISpecialDiscountRequestService _specialDiscountRequestService;
    private readonly IProductService _productService;

    public SellOrderController(ISellOrderService sellOrderService,
        ISpecialDiscountRequestService specialDiscountRequestService, IMapper mapper, IPointService pointService,
        IProductService productService)
    {
        _sellOrderService = sellOrderService;
        _specialDiscountRequestService = specialDiscountRequestService;
        _mapper = mapper;
        _pointService = pointService;
        _productService = productService;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAllAsync([FromQuery] List<string> statusList, bool ascending, int pageIndex,
        int pageSize)
    {
        var responseModel = await _sellOrderService.GetAllAsync(statusList, ascending, pageIndex, pageSize);
        return Ok(responseModel);
    }


    [HttpGet]
    [Route("GetById")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var responseModel = await _sellOrderService.GetByIdAsync(id);
        return Ok(responseModel);
    }

    [HttpPost]
    [Route("CreateOrder")]
    public async Task<IActionResult> CreateAsync([FromBody] RequestCreateSellOrder requestSellOrder)
    {
        var updatedOrder = new ResponseModel();
        var customerPhoneNumber = requestSellOrder.CustomerPhoneNumber;
        var staffId = requestSellOrder.StaffId;
        var createDate = requestSellOrder.CreateDate;
        ResponseModel specialDiscountModel;
        ResponseUpdateSellOrderWithSpecialPromotion response;

        //check if this is just an update, not create new order
        if (requestSellOrder.Id is not null)
        {
            var targetOrder = await _sellOrderService.MapOrderAsync(requestSellOrder);
            await _sellOrderService.RemoveAllSellOrderDetails(requestSellOrder.Id.Value);
            var result = await _sellOrderService.UpdateOrderAsync(targetOrder.Id, targetOrder);
            await _productService.UpdateAllProductStatusAsync(targetOrder, ProductConstants.InactiveStatus);
            return Ok($"Updated order {targetOrder.Id} successfully.");
        }

        //create truoc ti update lai special promotion sau
        var currentOrder = (SellOrder)(await _sellOrderService.CreateOrderAsync(requestSellOrder)).Data!;

        //handle order include special promotion 
        if (requestSellOrder.IsSpecialDiscountRequested)
        {
            var specialDiscountRate = requestSellOrder.SpecialDiscountRate!.Value;
            specialDiscountModel = await _specialDiscountRequestService.CreateAsync(new CreateSpecialDiscountRequest
            {
                CustomerPhoneNumber = customerPhoneNumber,
                StaffId = staffId,
                CreatedAt = createDate,
                DiscountRate = specialDiscountRate
            });
            var specialDiscountRequest = (SpecialDiscountRequest)specialDiscountModel.Data!;
            //set special discount request id
            requestSellOrder.SpecialDiscountRequestId = specialDiscountRequest.RequestId;
            //create sell order
            updatedOrder = await _sellOrderService.UpdateOrderAsync(currentOrder.Id, new RequestUpdateSellOrder
            {
                SpecialDiscountRequest = specialDiscountRequest,
                Status = OrderConstants.WaitingForDiscountResponseStatus
            });
            response = _mapper.Map<ResponseUpdateSellOrderWithSpecialPromotion>((SellOrder)updatedOrder.Data!);
        }
        else
        {
            response = _mapper.Map<ResponseUpdateSellOrderWithSpecialPromotion>(currentOrder);
        }

        //if no exception occurs
        await _pointService.DecreaseCustomerAvailablePointAsync(customerPhoneNumber, requestSellOrder.DiscountPoint);

        return Ok(response);
    }


    //Xai o trang customer approve gia cuoi cua order
    [HttpPut]
    [Route("UpdateStatus")]
    public async Task<IActionResult> UpdateSellOrderStatusAsync(int id,
        [FromBody] UpdateSellOrderStatus requestSellOrder)
    {
        var response = await _sellOrderService.UpdateStatusAsync(id, requestSellOrder);
        return Ok(response);
    }

    [HttpGet]
    [Route("SumTotalAmountOrderByDateTime")]
    public async Task<IActionResult> SumTotalAmountOrderAsync(DateTime startDate, DateTime endDate)
    {
        var responseModel = await _sellOrderService.SumTotalAmountOrderByDateTimeAsync(startDate, endDate);
        return Ok(responseModel);
    }

    [HttpGet]
    [Route("CountOrderByDateTime")]
    public async Task<IActionResult> CountOrderByDatetimeAsync(DateTime startDate, DateTime endDate)
    {
        var responseModel = await _sellOrderService.CountOrderByDateTimeAsync(startDate, endDate);
        return Ok(responseModel);
    }

    [HttpGet]
    [Route("Search")]
    public async Task<IActionResult> SearchAsync([FromQuery] List<string> statusList, [FromQuery] string customerPhone,
        bool ascending, int pageIndex, [FromQuery] int pageSize)
    {
        var responseModel =
            await _sellOrderService.SearchByAsync(statusList, customerPhone, ascending, pageIndex, pageSize);
        return Ok(responseModel);
    }
}