using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models.DiamondModel;
using JSSATSProject.Service.Models.DiamondPriceListModel;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JSSATSProject.API.Controllers
{
    [ApiController]
    public class DiamondPriceListController : ControllerBase
    {
        private readonly IDiamondPriceListService _diamondPriceListService;

        public DiamondPriceListController(IDiamondPriceListService diamondPriceListService)
        {
            _diamondPriceListService = diamondPriceListService;
        }

        [HttpGet]
        [Route("api/[controller]/GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _diamondPriceListService.GetAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("api/[controller]/GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseModel = await _diamondPriceListService.GetByIdAsync(id);
            return Ok(responseModel);
        }

        [HttpPost]
        [Route("api/[controller]/CreateDiamondPriceList")]
        public async Task<IActionResult> CreateAsync(RequestCreateDiamondPriceList requestDiamondPriceList)
        {
            var responseModel = await _diamondPriceListService.CreateDiamondPriceListAsync(requestDiamondPriceList);
            return Ok(responseModel);
        }

        [HttpPut]
        [Route("api/[controller]/UpdateDiamondPriceList")]
        public async Task<IActionResult> UpdateDiamondPriceListAsync(int id, [FromBody] RequestUpdateDiamondPriceList requestDiamondPriceList)
        {
            var response = await _diamondPriceListService.UpdateDiamondPriceListAsync(id, requestDiamondPriceList);
            return Ok(response);
        }
    }
}