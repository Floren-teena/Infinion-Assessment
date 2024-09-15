using Florentina_Infinion_Assessment.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florentina_Infinion_Assessment.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponseDto>> GetAllProducts(string filter, int page, int pageSize);
        Task<ProductResponseDto?> GetProductById(int id);
        Task AddProduct(ProductRequestDto productDto);
        Task UpdateProduct(int id, ProductRequestDto productDto);
        Task DeleteProduct(int id);
    }
}
