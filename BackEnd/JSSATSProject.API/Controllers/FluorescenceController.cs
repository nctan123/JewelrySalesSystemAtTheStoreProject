﻿using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FluorescenceController : ControllerBase
    {
        private readonly IFluorescenceService _fluorescenceService;

        public FluorescenceController(IFluorescenceService fluorescenceService)
        {
            _fluorescenceService = fluorescenceService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _fluorescenceService.GetAllAsync();
            return Ok(responseModel);
        }
    }
}
