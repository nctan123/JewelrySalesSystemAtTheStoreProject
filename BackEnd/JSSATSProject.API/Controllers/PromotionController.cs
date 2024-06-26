﻿using JSSATSProject.Service.Models.PromotionModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JSSATSProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;

        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync([FromQuery] int pageIndex = 1, [FromQuery] bool ascending = true)
        {
            try
            {
                var responseModel = await _promotionService.GetAllAsync(pageIndex, 10, ascending);
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPost]
        [Route("CreatePromotion")]
        public async Task<IActionResult> CreateAsync([FromBody] RequestCreatePromotion requestPromotion)
        {
            var responseModel = await _promotionService.CreatePromotionAsync(requestPromotion);
            return Ok(responseModel);
        }

        [HttpPut]
        [Route("UpdatePromotion")]
        public async Task<IActionResult> UpdatePromotionAsync(int Id, [FromBody] RequestUpdatePromotion requestPromotion)
        {
            var response = await _promotionService.UpdatePromotionAsync(Id, requestPromotion);
            return Ok(response);
        }



        [HttpGet]
        [Route("SearchPromotion")]
        public async Task<IActionResult> SearchPromotionsAsync([FromQuery] string searchTerm,[FromQuery] int pageIndex)
        {
            try
            {
                var responseModel = await _promotionService.SearchAsync(searchTerm, pageIndex, 10);
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}