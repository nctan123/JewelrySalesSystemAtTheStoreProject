using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll(int pageIndex, int pageSize)
        {
            var responseModel = await _customerService.GetAllAsync(pageSize,pageSize);
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var responseModel = await _customerService.GetByIdAsync(id);
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            var responseModel = await _customerService.GetByNameAsync(name);
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("GetByPhone")]
        public async Task<IActionResult> GetByPhone(string phonenumber)
        {
            var responseModel = await _customerService.GetByPhoneAsync(phonenumber);
            return Ok(responseModel);
        }

        [HttpPost]
        [Route("CreateCustomer")]
        public async Task<IActionResult> Create([FromBody] RequestCreateCustomer requestCustomer)
        {
            var responseModel = await _customerService.CreateCustomerAsync(requestCustomer);
            var data = (Customer)responseModel.Data!;
            return CreatedAtAction("GetById", new {id = data.Id} ,responseModel);
            
            // var responseModel = await _customerService.CreateCustomerAsync(requestCustomer);
            // return Ok(responseModel);
        }

        [HttpPut]
        [Route("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer(int Id, [FromBody] RequestUpdateCustomer requestCustomer)
        {
            var response = await _customerService.UpdateCustomerAsync(Id, requestCustomer);
            return Ok(response);
        }

        [HttpGet]
        [Route("CountNewCustomer")]
        public async Task<IActionResult> CountNewCustomer(DateTime startDate, DateTime endDate)
        {
            var responseModel = await _customerService.CountNewCustomer(startDate,endDate);
            return Ok(responseModel);
        }
    }
}