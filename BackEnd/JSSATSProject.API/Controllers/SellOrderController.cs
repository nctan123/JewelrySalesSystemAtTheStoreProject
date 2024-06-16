﻿using AutoMapper;
using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.OrderModel;
using JSSATSProject.Service.Models.SpecialDiscountRequestModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellOrderController : ControllerBase
    {
        private readonly ISellOrderService _sellOrderService;
        private readonly ISpecialDiscountRequestService _specialDiscountRequestService;
        private readonly IMapper _mapper;

        public SellOrderController(ISellOrderService sellOrderService,
            ISpecialDiscountRequestService specialDiscountRequestService, IMapper mapper)
        {
            _sellOrderService = sellOrderService;
            _specialDiscountRequestService = specialDiscountRequestService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _sellOrderService.GetAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseModel = await _sellOrderService.GetByIdAsync(id);
            return Ok(responseModel);
        }

        [HttpPost]
        [Route("CreateOrder")]
        public async Task<IActionResult> CreateAsync([FromBody] RequestCreateSellOrder requestSellOrder)
        {
            var updatedOrder = new ResponseModel();
            var customerPhoneNumber = requestSellOrder.CustomerPhoneNumber;
            var staffId = requestSellOrder.StaffId;
            var createDate = requestSellOrder.CreateDate;
            ResponseModel specialDiscountModel;
            ResponseUpdateSellOrderWithSpecialPromotion response;

            //create truoc ti update lai special promotion sau
            var currentOrder = (SellOrder)(await _sellOrderService.CreateOrderAsync(requestSellOrder)).Data!;

            //handle order include special promotion 
            if (requestSellOrder.IsSpecialDiscountRequested)
            {
                var specialDiscountRate = requestSellOrder.SpecialDiscountRate!.Value;
                specialDiscountModel = await _specialDiscountRequestService.CreateAsync(new CreateSpecialDiscountRequest
                {
                    CustomerPhoneNumber = customerPhoneNumber,
                    StaffId = staffId,
                    CreatedAt = createDate,
                    DiscountRate = specialDiscountRate
                });
                var specialDiscountRequest = (SpecialDiscountRequest)specialDiscountModel.Data!;
                //set special discount request id
                requestSellOrder.SpecialDiscountRequestId = specialDiscountRequest.RequestId;
                //create sell order
                updatedOrder = await _sellOrderService.UpdateOrderAsync(currentOrder.Id, new RequestUpdateSellOrder()
                {
                    SpecialDiscountRequest = specialDiscountRequest,
                    Status = OrderConstants.WaitingForDiscountResponseStatus
                });
                response = _mapper.Map<ResponseUpdateSellOrderWithSpecialPromotion>((SellOrder)updatedOrder.Data!);
            }
            else
            {
                response = _mapper.Map<ResponseUpdateSellOrderWithSpecialPromotion>(currentOrder);
            }


            return Ok(response);
        }


        //Xai o trang customer approve gia cuoi cua order
        [HttpPut]
        [Route("UpdateStatus")]
        public async Task<IActionResult> UpdateSellOrderStatusAsync(int id,
            [FromBody] UpdateSellOrderStatus requestSellOrder)
        {
            var response = await _sellOrderService.UpdateStatusAsync(id, requestSellOrder);
            return Ok(response);
        }

        [HttpGet]
        [Route("SumTotalAmountOrderByDateTime")]
        public async Task<IActionResult> SumTotalAmountOrderAsync(DateTime startDate, DateTime endDate)
        {
            var responseModel = await _sellOrderService.SumTotalAmountOrderByDateTimeAsync(startDate, endDate);
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("CountOrderByDateTime")]
        public async Task<IActionResult> CountOrderByDatetimeAsync(DateTime startDate, DateTime endDate)
        {
            var responseModel = await _sellOrderService.CountOrderByDateTimeAsync(startDate, endDate);
            return Ok(responseModel);
        }

       
    }
}