using JSSATSProject.Service.Models.ProductModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JSSATSProject.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
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
    public async Task<IActionResult> CreateProduct([FromForm] RequestCreateProduct requestProduct)
    {
        try
        {
            var response = await _productService.CreateProductAsync(requestProduct);
            return Ok(response);
        }
        catch (Exception ex)
        {
            // Return a generic error response
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }

    [HttpPut]
    [Route("UpdateStallProduct")]
    public async Task<IActionResult> UpdateProductAsync(int id, [FromBody] RequestUpdateProduct requestProduct)
    {
        var response = await _productService.UpdateStallProductAsync(id, requestProduct);
        return Ok(response);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllAsync(int categoryId, int? stallId = null, int pageIndex = 1, int pageSize = 10,
     bool ascending = true, bool includeNullStalls = true)
    { 
        try
        {
            var responseModel = await _productService.GetAllAsync(categoryId,stallId, pageIndex, pageSize, ascending, includeNullStalls);
            return Ok(responseModel);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpGet]
    [Route("Search")]
    public async Task<IActionResult> SearchProductsAsync(int categoryId, string searchTerm, int? stallId = null, int pageIndex = 1,
    int pageSize = 10, bool ascending = true, bool includeNullStalls = true)
    {
        var responseModel = await _productService.SearchProductsAsync(categoryId,searchTerm,stallId,pageIndex, pageSize, ascending, includeNullStalls);
        return Ok(responseModel);
    }

    [HttpPut]
    [Route("UpdateEnityProduct")]
    public async Task<IActionResult> UpdateEntityProductAsync(int id, [FromBody] RequestUpdateEntityProduct request)
    {
        var response = await _productService.UpdateEntityProductAsync(id, request);
        return Ok(response);
    }
}