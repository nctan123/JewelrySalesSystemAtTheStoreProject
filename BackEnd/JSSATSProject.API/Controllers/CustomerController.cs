using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers;

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
    public async Task<IActionResult> GetAll(int pageIndex = 1, int pageSize = 10)
    {
        var responseModel = await _customerService.GetAllAsync(pageIndex, pageSize);
        return Ok(responseModel);
    }

    [HttpGet]
    [Route("Search")]
    public async Task<IActionResult> Search(string searchTerm, int pageIndex = 1, int pageSize = 10)
    {
        var responseModel = await _customerService.SearchAsync(searchTerm, pageIndex, pageSize);
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
        //var data = (Customer)responseModel.Data!;
        return Ok(responseModel);
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
        var responseModel = await _customerService.CountNewCustomer(startDate, endDate);
        return Ok(responseModel);
    }
    
    [HttpGet]
    [Route("GetSellOrderByPhone")]
    public async Task<IActionResult> GetSellOrdersByPhoneAsync(string phoneNumber, int pageIndex, int pageSize)
    {
        var responseModel = await _customerService.GetSellOrdersByPhoneAsync(phoneNumber,pageIndex,pageSize);
        return Ok(responseModel);
    }

    [HttpGet]
    [Route("GetPaymentsByPhone")]
    public async Task<IActionResult> GetPaymentsByPhoneAsync(string phoneNumber, int pageIndex, int pageSize)
    {
        var responseModel = await _customerService.GetPaymentsByPhoneAsync(phoneNumber, pageIndex, pageSize);
        return Ok(responseModel);
    }

    [HttpGet]
    [Route("GetBuyOrdersByPhone")]
    public async Task<IActionResult> GetBuyOrdersByPhoneAsync(string phoneNumber, int pageIndex, int pageSize)
    {
        var responseModel = await _customerService.GetBuyOrdersByPhoneAsync(phoneNumber, pageIndex, pageSize);
        return Ok(responseModel);
    }


}