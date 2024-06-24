using JSSATSProject.Service.Models.OrderDetail;
using JSSATSProject.Service.Models.OrderModel;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Mvc;
using System;

namespace JSSATSProject.API.Controllers
{
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
        public async Task<IActionResult> UpdateOrderDetailAsync(int id, [FromBody] RequestUpdateOrderDetail requestOrderDetail)
        {
            var response = await _sellorderdetailService.UpdateOrderDetailAsync(id, requestOrderDetail);
            return Ok(response);
        }

        [HttpGet]
        [Route("CountProductsSoldByCategory")]
        public async Task<IActionResult> CountProductsSoldByCategoryAsync(DateTime startDate, DateTime endDate)
        {
            var responseModel = await _sellorderdetailService.CountProductsSoldByCategoryAsync(startDate,endDate);
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("GetTotalRevenueStall")]
        public async Task<IActionResult> GetTotalRevenueStallAsync(DateTime startDate, DateTime endDate)
        {
            var responseModel = await _sellorderdetailService.GetTotalRevenueStallAsync(startDate, endDate);
            return Ok(responseModel);
        }


    }
}
