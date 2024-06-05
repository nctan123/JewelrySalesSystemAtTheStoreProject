using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [Route("api/[controller]/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var responseModel = await _customerService.GetAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("api/[controller]/GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var responseModel = await _customerService.GetByIdAsync(id);
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("api/[controller]/GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            var responseModel = await _customerService.GetByNameAsync(name);
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("api/[controller]/GetByPhone")]
        public async Task<IActionResult> GetByPhone(string phonenumber)
        {
            var responseModel = await _customerService.GetByPhoneAsync(phonenumber);
            return Ok(responseModel);
        }

        [HttpPost]
        [Route("api/[controller]/CreateCustomer")]
        public async Task<IActionResult> Create([FromBody] RequestCreateCustomer requestCustomer)
        {
            var responseModel = await _customerService.CreateCustomerAsync(requestCustomer);
            return Ok(responseModel);
        }

        [HttpPut]
        [Route("api/[controller]/UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer(int Id, [FromBody] RequestUpdateCustomer requestCustomer)
        {
            var response = await _customerService.UpdateCustomerAsync(Id, requestCustomer);
            return Ok(response);
        }
    }
}