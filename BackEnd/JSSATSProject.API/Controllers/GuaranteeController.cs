using System.Net;
using JSSATSProject.Service.Models.CustomerModel;
using JSSATSProject.Service.Models.GuaranteeModel;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Service.Service.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models.ProductModel;

namespace JSSATSProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class GuaranteeController : ControllerBase
    {
        private readonly IGuaranteeService _guaranteeService;

        public GuaranteeController(IGuaranteeService guaranteeService)
        {
            _guaranteeService = guaranteeService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var responseModel = await _guaranteeService.GetAllAsync();
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var responseModel = await _guaranteeService.GetByIdAsync(id);
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("GetByProductId")]
        public async Task<IActionResult> GetByProductIdAsync(int productId)
        {
            var responseModel = await _guaranteeService.GetByProductIdAsync(productId);
            return Ok(responseModel);
        }

        [HttpPut]
        [Route("UpdateGuarantee")]
        public async Task<IActionResult> UpdateAsync(int guaranteeId, [FromBody] RequestUpdateGuarantee requestGuarantee)
        {
            var responseModel = await _guaranteeService.UpdateGuaranteeAsync(guaranteeId, requestGuarantee);
            return Ok(responseModel);
        }

        [HttpPost]
        [Route("CheckGuarantee")]
        public async Task<IActionResult> CheckAsync(string guaranteeCode)
        {
            var guarantee = await _guaranteeService.GetEntityByCodeAsync(guaranteeCode);
            if (guarantee is null)
                return Problem(statusCode: Convert.ToInt32(HttpStatusCode.BadRequest),
                    title: "Guarantee not found.",
                    detail: $"Cannot find data of order {guaranteeCode}");
            //map sp trong sellOrder details thanh response product dto
            ResponseProductForCheckOrder product = _guaranteeService.GetResponseProductForCheckOrder(guarantee);
            return Ok(new ResponseCheckGuarantee()
                {
                    Code = guaranteeCode,
                    CustomerName = string.Join(" ", guarantee.SellOrderDetail.Order.Customer.Firstname, guarantee.SellOrderDetail.Order.Customer.Lastname),
                    CustomerPhoneNumber = guarantee.SellOrderDetail.Order.Customer.Phone,
                    EffectiveDate = guarantee.EffectiveDate,
                    ExpireDate = guarantee.ExpiredDate,
                    Product = product
                }
            );
            return Ok();
        }
    }
}