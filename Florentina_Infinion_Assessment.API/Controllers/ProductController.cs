using Florentina_Infinion_Assessment.Application.DTOs;
using Florentina_Infinion_Assessment.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> GetAllProducts([FromQuery] string? filter, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var products = await _productService.GetAllProducts(filter!, page, pageSize);
            return Ok(products);
        }

        [HttpGet("Get-products-by-id/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound(new { Message = "Product not found" });
            }
            return Ok(product);
        }

        [HttpPost("Add-products")]
        public async Task<IActionResult> AddProduct([FromBody] ProductRequestDto productDto)
        {
            await _productService.AddProduct(productDto);
            return CreatedAtAction(nameof(GetProductById), new { id = productDto.Name }, productDto);
        }

        [HttpPut("Update-product/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductRequestDto productDto)
        {
            await _productService.UpdateProduct(id, productDto);
            return NoContent();
        }

        [HttpDelete("Delete-product/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProduct(id);
            return NoContent();
        }
    }
}
