using JSSATSProject.Service.Models.ProductModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    [Route("GetByCode")]
    public async Task<IActionResult> GetByCodeAsync(string code)
    {
        var responseModel = await _productService.GetByCodeAsync(code);
        return Ok(responseModel);
    }

    [HttpPost]
    [Route("CreateProduct")]
    public async Task<IActionResult> CreateAsync([FromBody] RequestCreateProduct requestProduct)
    {
        var responseModel = await _productService.CreateProductAsync(requestProduct);
        return Ok(responseModel);
    }

    [HttpPut]
    [Route("UpdateStallProduct")]
    public async Task<IActionResult> UpdateProductAsync(int id, [FromBody] RequestUpdateProduct requestProduct)
    {
        var response = await _productService.UpdateStallProductAsync(id, requestProduct);
        return Ok(response);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllAsync(int categoryId, int pageIndex = 1, int pageSize = 10, bool ascending = true, bool includeNullStalls = true)
    {
        try
        {
            var responseModel = await _productService.GetAllAsync(categoryId, pageIndex, pageSize, ascending, includeNullStalls);
            return Ok(responseModel);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpGet]
    [Route("Search")]
    public async Task<IActionResult> SearchProductsAsync(int categoryId, string searchTerm, int pageIndex = 1, int pageSize = 10, bool ascending = true, bool includeNullStalls = true)
    {
        var responseModel = await _productService.SearchProductsAsync(categoryId, searchTerm, pageIndex, pageSize, ascending, includeNullStalls);
        return Ok(responseModel);
    }
}