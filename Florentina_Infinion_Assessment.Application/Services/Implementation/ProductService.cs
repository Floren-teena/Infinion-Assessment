using Florentina_Infinion_Assessment.Application.DTOs;
using Florentina_Infinion_Assessment.Application.Services.Interfaces;
using Florentina_Infinion_Assessment.Core.Models;
using Florentina_Infinion_Assessment.Infrastructure.Repositories.Interfaces;

namespace Florentina_Infinion_Assessment.Application.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllProducts(string filter, int page, int pageSize)
        {
            var products = await _productRepository.GetAllProducts(filter, page, pageSize);
            return products.Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            }).ToList();
        }

        public async Task<ProductResponseDto?> GetProductById(int id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null) return null;
            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }

        public async Task AddProduct(ProductRequestDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price
            };
            await _productRepository.AddProduct(product);
        }

        public async Task<bool> UpdateProduct(int id, ProductRequestDto productDto)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null) { return false; }
            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;

            return await _productRepository.UpdateProduct(product);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            return await _productRepository.DeleteProduct(id);
        }

    }
}
