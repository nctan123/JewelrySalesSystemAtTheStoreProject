using System.Net;
using AutoMapper;
using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Service.Models.OrderModel;
using JSSATSProject.Service.Models.ProductModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyOrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISellOrderService _sellOrderService;
        private readonly IBuyOrderService _buyOrderService;

        public BuyOrderController(IMapper mapper, ISellOrderService sellOrderService, IBuyOrderService buyOrderService)
        {
            _mapper = mapper;
            _sellOrderService = sellOrderService;
            _buyOrderService = buyOrderService;
        }

        // [HttpPost]
        // [Route("CreateOrder")]
        // public IActionResult CreateAsync([FromBody] )
        // {
        //     
        // }

        [HttpPost]
        [Route("CheckOrder")]
        public async Task<IActionResult> CheckAsync(string orderCode)
        {
            if (orderCode.StartsWith(OrderConstants.SellOrderCodePrefix))
            {
                var sellOrder = await _sellOrderService.GetEntityByCodeAsync(orderCode);
                if (sellOrder is null)
                    return Problem(statusCode: Convert.ToInt32(HttpStatusCode.BadRequest),
                        title: "Order not found.",
                        detail: $"Cannot find data of order {orderCode}");
                //map sp trong sellOrder details thanh response product dto
                var products = _sellOrderService.GetProducts(sellOrder);
                return Ok(new ResponseCheckOrder()
                    {
                        code = orderCode,
                        CustomerName = string.Join(" ", sellOrder.Customer.Firstname, sellOrder.Customer.Lastname),
                        CustomerPhoneNumber = sellOrder.Customer.Phone,
                        CreateDate = sellOrder.CreateDate,
                        TotalValue = sellOrder.TotalAmount,
                        Products = products
                    }
                );
            }

            return Problem(statusCode: Convert.ToInt32(HttpStatusCode.BadRequest),
                title: "Order type is invalid.",
                detail: "The system can just buyback product from Sell Orders.");
        }
    }
}