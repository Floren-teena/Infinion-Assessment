using Florentina_Infinion_Assessment.Application.DTOs;
using Florentina_Infinion_Assessment.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Florentina_Infinion_Assessment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("Get-all-products")]
        public async Task<IActionResult> GetAllProducts([FromQuery] string? filter, [FromQuery] int page = 1, [FromQuery] int pageSize = 2)
        {
            var products = await _productService.GetAllProducts(filter!, page, pageSize);
            return Ok(products);
        }

        [HttpGet("Get-products-by-id-{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null) { return NotFound(new { Message = "Product not found" }); }
            return Ok(product);
        }

        [HttpPost("Add-products")]
        public async Task<IActionResult> AddProduct([FromBody] ProductRequestDto productDto)
        {
            await _productService.AddProduct(productDto);
            return CreatedAtAction(nameof(GetProductById), new { id = productDto.Name }, productDto);
        }

        [HttpPut("Update-product-{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductRequestDto productDto)
        {
            if (productDto == null) { return BadRequest("Invalid product data."); }
            var isUpdated = await _productService.UpdateProduct(id, productDto);
            if (isUpdated) { return Ok("Product updated successfully"); }
            return NotFound("Product could not be updated or does not exist.");
        }

        [HttpDelete("Delete-product-{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var isDeleted = await _productService.DeleteProduct(id);
            if (isDeleted) { return Ok("Product deleted successfully."); }
            return NotFound("Product could not be deleted or does not exist.");
        }

    }
}
