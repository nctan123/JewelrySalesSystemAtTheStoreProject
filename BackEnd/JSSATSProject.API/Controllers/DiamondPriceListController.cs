
using JSSATSProject.Service.Models.DiamondPriceListModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JSSATSProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiamondPriceListController : ControllerBase
    {
        private readonly IDiamondPriceListService _diamondPriceListService;

        public DiamondPriceListController(IDiamondPriceListService diamondPriceListService)
        {
            _diamondPriceListService = diamondPriceListService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _diamondPriceListService.GetAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseModel = await _diamondPriceListService.GetByIdAsync(id);
            return Ok(responseModel);
        }

        /// <summary>
        /// This method must returns an appropriate status code and the created model in response body
        /// </summary>
        /// <param name="requestDiamondPriceList"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateDiamondPriceList")]
        public async Task<IActionResult> CreateAsync(RequestCreateDiamondPriceList requestDiamondPriceList)
        {
            // var responseModel = await _diamondPriceListService.CreateDiamondPriceListAsync(requestDiamondPriceList);
            // var data = (DiamondPriceList)responseModel.Data!;
            // return CreatedAtAction("GetByIdAsync", data.Id ,responseModel);
            var responseModel = await _diamondPriceListService.CreateDiamondPriceListAsync(requestDiamondPriceList);
            return Ok(responseModel);
        }

        [HttpPut]
        [Route("UpdateDiamondPriceList")]
        public async Task<IActionResult> UpdateDiamondPriceListAsync(int id, [FromBody] RequestUpdateDiamondPriceList requestDiamondPriceList)
        {
            var response = await _diamondPriceListService.UpdateDiamondPriceListAsync(id, requestDiamondPriceList);
            return Ok(response);
        }
    }
}