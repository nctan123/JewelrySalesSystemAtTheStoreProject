using JSSATSProject.Service.Models.OrderDetail;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SellOrderDetailController : ControllerBase
{
    private readonly ISellOrderDetailService _sellorderdetailService;

    public SellOrderDetailController(ISellOrderDetailService orderdetailService)
    {
        _sellorderdetailService = orderdetailService;
    }

    [HttpGet]
    [Route("GetByOrderId")]
    public async Task<IActionResult> GetByOrderIdAsync(int id)
    {
        var responseModel = await _sellorderdetailService.GetByOrderIdAsync(id);
        return Ok(responseModel);
    }

    [HttpPost]
    [Route("CreateOrderDetail")]
    public async Task<IActionResult> CreateAsync([FromBody] RequestCreateOrderDetail requestOrderDetail)
    {
        var responseModel = await _sellorderdetailService.CreateOrderDetailAsync(requestOrderDetail);
        return Ok(responseModel);
    }

    [HttpPut]
    [Route("UpdateOrderDetail")]
    public async Task<IActionResult> UpdateOrderDetailAsync(int id,
        [FromBody] RequestUpdateOrderDetail requestOrderDetail)
    {
        var response = await _sellorderdetailService.UpdateOrderDetailAsync(id, requestOrderDetail);
        return Ok(response);
    }

    [HttpGet]
    [Route("CountProductsSoldByCategory")]
    public async Task<IActionResult> CountProductsSoldByCategoryAsync(DateTime startDate, DateTime endDate)
    {
        var responseModel = await _sellorderdetailService.CountProductsSoldByCategoryAsync(startDate, endDate);
        return Ok(responseModel);
    }


    [HttpGet]
    [Route("GetProductSold")]
    public async Task<IActionResult> GetProductSoldAsync(bool ascending, int pageIndex, int pageSize)
    {
        var responseModel = await _sellorderdetailService.GetProductSoldAsync(ascending, pageIndex, pageSize);
        return Ok(responseModel);
    }


    [HttpGet]
    [Route("GetProductsByStall")]
    public async Task<IActionResult> GetProductsByStallAsync(int stallId,DateTime startDate, DateTime endDate, int pageIndex,int pageSize,
        bool ascending = false)
    {
        var responseModel =
            await _sellorderdetailService.GetProductsByStallAsync(stallId,startDate, endDate, pageIndex, pageSize, ascending);
        return Ok(responseModel);
    }
}