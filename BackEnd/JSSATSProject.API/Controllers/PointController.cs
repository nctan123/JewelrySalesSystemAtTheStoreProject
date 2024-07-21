using Azure;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.PointModel;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers;
//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PointController : ControllerBase
{
    private readonly IPointService _pointService;
    private readonly ISellOrderService _sellOrderService;

    public PointController(IPointService pointService, ISellOrderService sellOrderService)
    {
        _pointService = pointService;
        _sellOrderService = sellOrderService;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAllAsync()
    {
        var responseModel = await _pointService.GetAllAsync();
        return Ok(responseModel);
    }

    [HttpGet]
    [Route("GetById")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var responseModel = await _pointService.GetByIdAsync(id);
        return Ok(responseModel);
    }


    [HttpPost]
    [Route("CreatePoint")]
    public async Task<IActionResult> CreateAsync([FromBody] RequestCreatePoint requestPoint)
    {
        var responseModel = await _pointService.CreatePointAsync(requestPoint);
        return Ok(responseModel);
    }

    [HttpPut]
    [Route("UpdatePoint")]
    public async Task<IActionResult> UpdatePointAsync(int orderId)
    {
        var sellorder = await _sellOrderService.GetEntityByIdAsync(orderId);
        var discountPoint = sellorder.DiscountPoint;
        var customerPhone = sellorder.Customer.Phone;
        var sellorderAmount = await _sellOrderService.GetFinalPriceAsync(sellorder);
        await _pointService.AddCustomerPoint(customerPhone, sellorderAmount);
        return Ok();
    }


   
    [HttpPost]
    [Route("GetPointForOrder")]
    public async Task<IActionResult> GetPointForOrder([FromBody] RequestGetPointForOrder requestGetPointForOrder)
    {
        var applicablePoint = await _pointService.GetMaximumApplicablePointForOrder(
            requestGetPointForOrder.CustomerPhoneNumber, requestGetPointForOrder.TotalOrderPrice);
        var pointToCurrencyRate = await _pointService.GetPointToCurrencyConversionRate(DateTime.Now);
        return Ok(new ResponseModel()
        {
            Data = new
            {
                applicablePoint = applicablePoint,
                pointToCurrencyRate = pointToCurrencyRate
            }
        });
    }

}