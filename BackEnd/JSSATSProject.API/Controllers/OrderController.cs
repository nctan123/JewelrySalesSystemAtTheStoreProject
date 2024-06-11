<<<<<<< HEAD
﻿using System.Net;
using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models.OrderModel;
=======
﻿using JSSATSProject.Service.Models.OrderModel;
>>>>>>> ef1d898c610203bb40990ce34f1644abc601b704
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using System.Threading.Tasks;
using JSSATSProject.Repository;
using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Service.Models;
=======
>>>>>>> ef1d898c610203bb40990ce34f1644abc601b704

namespace JSSATSProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IStaffService _staffService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;

        public OrderController(IOrderService orderService, IStaffService staffService, IProductService productService,
            ICustomerService customerService)
        {
            _orderService = orderService;
            _staffService = staffService;
            _productService = productService;
            _customerService = customerService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _orderService.GetAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseModel = await _orderService.GetByIdAsync(id);
            return Ok(responseModel);
        }

        [HttpPost]
        [Route("CreateOrder/sell")]
        public async Task<IActionResult> CreateAsync([FromBody] RequestCreateOrder requestOrder)
        {
            var validateProductsResult = await _productService.AreValidProducts(requestOrder.ProductCodes);
            if (!validateProductsResult)
                return Problem(statusCode: Convert.ToInt32(HttpStatusCode.Forbidden), 
                    title: "Entered Product data issues",
                    detail: "Provided product codes or quantity is invalid.");

            var customer = await _customerService.FindByPhoneNumber(requestOrder.CustomerPhoneNumber);
            if (!customer.MessageError.Equals(Constants.Success))
            {
                return customer.MessageError switch
                {
                    Constants.CustomerNotFound =>
                        Problem(statusCode: Convert.ToInt32(HttpStatusCode.Forbidden),
                            title: "Entered Customer data issues",
                            detail: "Customer is not exist."),

                    Constants.InvalidPhoneNumberFormat => 
                        Problem(statusCode: Convert.ToInt32(HttpStatusCode.Forbidden),
                        title: "Entered Phone Number data issues",
                        detail: "Phone number format is invalid."),

                    _ => Problem(statusCode: Convert.ToInt32(HttpStatusCode.Forbidden),
                        title: "Unknown error",
                        detail: "An unknown error occured in the process.")
                };
            }
            
            // var responseModel = await _orderService.CreateOrderAsync(requestOrder);
            // return Ok(responseModel);
            return Ok("dep trai");
        }

        [HttpPut]
        [Route("UpdateOrder")]
        public async Task<IActionResult> UpdateOrderAsync(int id, [FromBody] RequestUpdateOrder requestOrder)
        {
            var response = await _orderService.UpdateOrderAsync(id, requestOrder);
            return Ok(response);
        }
    }
}