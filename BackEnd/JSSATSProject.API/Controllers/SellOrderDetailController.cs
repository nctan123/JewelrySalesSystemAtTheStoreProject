using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;
using System;

namespace JSSATSProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellOrderDetailController : ControllerBase
    {
        private readonly ISellOrderDetailService _orderdetailService;

        public SellOrderDetailController(ISellOrderDetailService orderdetailService)
        {
            _orderdetailService = orderdetailService;
        }


        [HttpGet]
        [Route("CountProductsSoldByCategory")]
        public async Task<IActionResult> CountProductsSoldByCategoryAsync(DateTime startDate, DateTime endDate)
        {
            var responseModel = await _orderdetailService.CountProductsSoldByCategoryAsync(startDate,endDate);
            return Ok(responseModel);
        }
    }
}
