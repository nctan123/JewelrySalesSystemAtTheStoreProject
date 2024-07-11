using System.Net;
using AutoMapper;
using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models.BuyOrderModel;
using JSSATSProject.Service.Models.OrderModel;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BuyOrderController : ControllerBase
{
    private readonly IBuyOrderService _buyOrderService;
    private readonly ICustomerService _customerService;
    private readonly IMapper _mapper;
    private readonly ISellOrderService _sellOrderService;
    private readonly IBuyOrderDetailService _buyOrderDetailService;

    public BuyOrderController(IMapper mapper, ISellOrderService sellOrderService, IBuyOrderService buyOrderService,
        ICustomerService customerService, IBuyOrderDetailService buyOrderDetailService)
    {
        _mapper = mapper;
        _sellOrderService = sellOrderService;
        _buyOrderService = buyOrderService;
        _customerService = customerService;
        _buyOrderDetailService = buyOrderDetailService;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAllAsync([FromQuery] List<string> statusList, bool ascending, int pageIndex,
        int pageSize)
    {
        var responseModel = await _buyOrderService.GetAllAsync(statusList, ascending, pageIndex, pageSize);
        return Ok(responseModel);
    }

    [HttpGet]
    [Route("Search")]
    public async Task<IActionResult> SearchAsync([FromQuery] List<string> statusList, [FromQuery] string customerPhone,
        bool ascending, int pageIndex, [FromQuery] int pageSize)
    {
        var responseModel =
            await _buyOrderService.SearchByCriteriaAsync(statusList, customerPhone, ascending, pageIndex, pageSize);

        return Ok(responseModel);
    }

    [HttpGet]
    [Route("CheckOrder")]
    public async Task<IActionResult> CheckAsync(string orderCode)
    {
        if (orderCode.StartsWith(OrderConstants.SellOrderCodePrefix))
        {
            var sellOrder = await _sellOrderService.GetEntityByCodeAsync(orderCode);
            if (sellOrder is null || !sellOrder.Status.Equals(OrderConstants.CompletedStatus))
            {
                return Problem(statusCode: Convert.ToInt32(HttpStatusCode.BadRequest),
                    title: "Order not found.",
                    detail: $"Cannot find data of order {orderCode}");
            }

            //map sp trong sellOrder details thanh response product dto
            var products = await _sellOrderService.GetProducts(sellOrder);
            return Ok(new ResponseCheckOrder
                {
                    code = orderCode,
                    CustomerName = string.Join(" ", sellOrder.Customer.Firstname, sellOrder.Customer.Lastname),
                    CustomerPhoneNumber = sellOrder.Customer.Phone,
                    CreateDate = sellOrder.CreateDate,
                    TotalValue = await _sellOrderService.GetFinalPriceAsync(sellOrder) ,
                    Products = products
                }
            );
        }

        return Problem(statusCode: Convert.ToInt32(HttpStatusCode.BadRequest),
            title: "Order type is invalid.",
            detail: "The system can just check buyback products from Sell Orders.");

    }

    [HttpPost]
    [Route("CreateInCompanyOrder")]
    public async Task<IActionResult> CreateInCompanyOrder([FromBody] RequestCreateBuyOrder requestCreateBuyOrder)
    {
        //assume that all data are already valid
        var customer =
            (Customer)(await _customerService.GetEntityByPhoneAsync(requestCreateBuyOrder.CustomerPhoneNumber)).Data!;

        var buyOrder = new BuyOrder
        {
            CustomerId = customer.Id,
            StaffId = requestCreateBuyOrder.StaffId,
            Status = "processing",
            TotalAmount = _buyOrderService.GetTotalAmount(requestCreateBuyOrder.ProductCodesAndQuantity,
                requestCreateBuyOrder.ProductCodesAndEstimatePrices),
            CreateDate = requestCreateBuyOrder.CreateDate,
            Description = requestCreateBuyOrder.Description,
            BuyOrderDetails = null
        };

        //save buyOrder
        await _buyOrderService.CreateAsync(buyOrder);
        buyOrder.BuyOrderDetails = await _buyOrderService.CreateOrderDetails(requestCreateBuyOrder, buyOrder.Id);
        var result = (await _buyOrderService.UpdateAsync(buyOrder.Id, buyOrder)).Data;
        return Ok(result);
    }

    [HttpPost]
    [Route("CreateNonCompanyOrder")]
    public async Task<IActionResult> CreateNonCompanyOrder(
        [FromBody] RequestCreateNonCompanyBuyOrder requestCreateBuyOrder)
    {
        //assume that all data are already valid
        var customer =
            (Customer)(await _customerService.GetEntityByPhoneAsync(requestCreateBuyOrder.CustomerPhoneNumber)).Data!;
        var buyOrder = new BuyOrder()
        {
            CustomerId = customer.Id,
            StaffId = requestCreateBuyOrder.StaffId,
            Status = "processing",
            CreateDate = requestCreateBuyOrder.CreateDate,
            Description = requestCreateBuyOrder.Description,
            BuyOrderDetails = null
        };
        await _buyOrderService.CreateAsync(buyOrder);
        buyOrder.BuyOrderDetails = await _buyOrderService.CreateOrderDetails(requestCreateBuyOrder, buyOrder.Id);
        buyOrder.TotalAmount = buyOrder.BuyOrderDetails.Sum(bo => bo.UnitPrice);
        var result = (await _buyOrderService.UpdateAsync(buyOrder.Id, buyOrder)).Data;
        return Ok(result);
    }


    [HttpPut]
    [Route("UpdateStatus")]
    public async Task<IActionResult> UpdateStatus(int orderId, [FromBody]RequestUpdateBuyOrderStatus buyOrder)
    {
        var result = await _buyOrderService.UpdateAsync(orderId, buyOrder);
        return Ok(result);
    }

    [HttpGet]
    [Route("GetById")]
    public async Task<IActionResult> GetById(int Id)
    {
        var result = await _buyOrderService.GetByIdAsync(Id);
        return Ok(result);
    }

    [HttpGet]
    [Route("SumTotalAmountOrderByDateTime")]
    public async Task<IActionResult> SumTotalAmountOrderAsync(DateTime startDate, DateTime endDate)
    {
        var responseModel = await _buyOrderService.SumTotalAmountOrderByDateTimeAsync(startDate, endDate);
        return Ok(responseModel);
    }

    [HttpGet]
    [Route("CountOrderByDateTime")]
    public async Task<IActionResult> CountOrderByDatetimeAsync(DateTime startDate, DateTime endDate)
    {
        var responseModel = await _buyOrderService.CountOrderByDateTimeAsync(startDate, endDate);
        return Ok(responseModel);
    }

    [HttpGet]
    [Route("CountProductsSoldByCategory")]
    public async Task<IActionResult> CountProductsSoldByCategoryAsync(DateTime startDate, DateTime endDate)
    {
        var responseModel = await _buyOrderDetailService.CountProductsSoldByCategoryAsync(startDate, endDate);
        return Ok(responseModel);
    }
}
