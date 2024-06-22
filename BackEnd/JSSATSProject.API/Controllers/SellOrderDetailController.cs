using JSSATSProject.Service.Service.IService;
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
