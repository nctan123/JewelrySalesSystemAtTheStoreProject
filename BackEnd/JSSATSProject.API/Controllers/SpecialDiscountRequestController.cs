using Azure;
using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Repository.CustomLib;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models.OrderModel;
using JSSATSProject.Service.Models.SellOrderModel;
using JSSATSProject.Service.Models.SpecialDiscountRequestModel;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace JSSATSProject.API.Controllers;
[Authorize(RoleConstants.Manager + "," +  RoleConstants.Seller)]
[ApiController]
[Route("api/[controller]")]
public class SpecialDiscountRequestController : ControllerBase
{
    private readonly ISellOrderService _sellOrderService;
    private readonly ISpecialDiscountRequestService _specialdiscountrequestService;

    public SpecialDiscountRequestController(ISpecialDiscountRequestService specialdiscountrequestService,
        ISellOrderService sellOrderService)
    {
        _specialdiscountrequestService = specialdiscountrequestService;
        _sellOrderService = sellOrderService;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAllAsync(bool ascending, int pageIndex, int pageSize)
    {
        var responseModel =
            await _specialdiscountrequestService.GetAllAsync(ascending, pageIndex, pageSize);
        return Ok(responseModel);
    }

    //[HttpGet]
    //[Route("Search")]
    //public async Task<IActionResult> GetAsync(string orderCode)
    //{
    //    var responseModel =
    //        await _specialdiscountrequestService.GetAsync(orderCode);
    //    return Ok(responseModel);
    //}

    [HttpGet]
    [Route("GetByCustomerId")]
    public async Task<IActionResult> GetByCustomerIdAsync(int id)
    {
        var responseModel = await _specialdiscountrequestService.GetByCustomerIdAsync(id);
        return Ok(responseModel);
    }


    [HttpPost]
    [Route("CreateSpecialDiscountRequest")]
    public async Task<IActionResult> CreateSpecialDiscountRequestAsync(
        CreateSpecialDiscountRequest specialdiscountRequest)
    {
        var responseModel = await _specialdiscountrequestService.CreateAsync(specialdiscountRequest);
        return Ok(responseModel);
    }

    [HttpPut]
    [Route("UpdateSpecialDiscountRequest")]
    public async Task<IActionResult> UpdateSpecialDiscountRequestAsync([FromQuery] int id,
        [FromBody] UpdateSpecialDiscountRequest specialdiscountRequest)
    {
        var response = await _specialdiscountrequestService.UpdateAsync(id, specialdiscountRequest);
        var targetEntity = await _specialdiscountrequestService.GetEntityByIdAsync(id);
        var sellOrderId = targetEntity!.SellOrders.First().Id;
        if (specialdiscountRequest.Status == SpecialDiscountRequestConstants.ApprovedStatus)
        {
            await _sellOrderService.UpdateOrderAsync(sellOrderId, new RequestUpdateSellOrder
            {
                SpecialDiscountRequest = targetEntity,
                Status = OrderConstants.WaitingForCustomer
            });
        }
        else
        {

            await _sellOrderService.UpdateOrderAsync(sellOrderId, new RequestUpdateSellOrder
            {
                SpecialDiscountRequest = targetEntity,
                Status = OrderConstants.WaitingForCustomer
            });
        }
        return Ok(response);
    }

    [HttpGet]
    [Route("Search")]
    public async Task<IActionResult> Search(string searchTerm = "", bool ascending = true, int pageIndex = 1,
        int pageSize = 10)
    {
        var responseModel =
            await _specialdiscountrequestService.SearchAsync(searchTerm, ascending, pageIndex, pageSize);
        return Ok(responseModel);
    }

    [HttpPut]
    [Route("UpdateBySellOrder")]
    public async Task<IActionResult> UpdateBySellOrderAsync(int orderId)
    {
        var sellorder = await _sellOrderService.GetEntityByIdAsync(orderId);
        var specialDiscount = sellorder.SpecialDiscountRequestId;
        if (specialDiscount != null)
        {
            var specialDiscountId = sellorder.SpecialDiscountRequest.RequestId;
            var newspecialdiscount = new UpdateSpecialDiscountRequest
            {
                DiscountRate = sellorder.SpecialDiscountRequest?.DiscountRate ?? 0,
                Status = "used",
                ApprovedBy = sellorder.SpecialDiscountRequest.ApprovedBy ?? 0
            };
            await _specialdiscountrequestService.UpdateAsync(specialDiscountId, newspecialdiscount);
        }
        return Ok();
    }
}