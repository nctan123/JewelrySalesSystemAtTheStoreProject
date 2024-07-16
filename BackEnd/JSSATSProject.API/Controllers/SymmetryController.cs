using JSSATSProject.Service.Service.IService;
﻿using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers
{
    [Authorize(Roles = RoleConstants.Manager)]
    [ApiController]
    [Route("api/[controller]")]
    public class SymmetryController : ControllerBase
    {
        private readonly ISymmetryService _symmetryService;

        public SymmetryController(ISymmetryService symmetryService)
        {
            _symmetryService = symmetryService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _symmetryService.GetAllAsync();
            return Ok(responseModel);
        }
    }
}
