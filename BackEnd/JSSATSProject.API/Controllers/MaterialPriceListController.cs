using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models.MaterialPriceListModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JSSATSProject.API.Controllers
{
    public class MaterialPriceListController : ControllerBase
    {
        private readonly IMaterialPriceListService _materialPriceListService;

        public MaterialPriceListController(IMaterialPriceListService materialPriceListService)
        {
            _materialPriceListService = materialPriceListService;
        }

        [HttpGet]
        [Route("api/[controller]/GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _materialPriceListService.GetAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("api/[controller]/GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseModel = await _materialPriceListService.GetByIdAsync(id);
            return Ok(responseModel);
        }

        [HttpPost]
        [Route("api/[controller]/CreateMaterialPriceList")]
        public async Task<IActionResult> CreateAsync([FromBody] RequestCreateMaterialPriceList requestMaterialPriceList)
        {
            var responseModel = await _materialPriceListService.CreateMaterialPriceListAsync(requestMaterialPriceList);
            return Ok(responseModel);
        }

        [HttpPut]
        [Route("api/[controller]/UpdateMaterialPriceList")]
        public async Task<IActionResult> UpdateMaterialPriceListAsync(int Id, [FromBody] RequestUpdateMaterialPriceList requestMaterialPriceList)
        {
            var response = await _materialPriceListService.UpdateMaterialPriceListAsync(Id, requestMaterialPriceList);
            return Ok(response);
        }
    }
}