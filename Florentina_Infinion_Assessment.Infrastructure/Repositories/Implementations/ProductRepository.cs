using Florentina_Infinion_Assessment.Core.Models;
using Florentina_Infinion_Assessment.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florentina_Infinion_Assessment.Infrastructure.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProductRepository(ApplicationDbContext applicationDbContext) 
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Product>> GetAllProducts(string filter, int page, int pageSize)
        {
            var query = _applicationDbContext.Products.AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(p => p.Name.Contains(filter));
            }
            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Product?> GetProductById(int id)
        {
            return await _applicationDbContext.Products.FindAsync(id);
        }

        public async Task AddProduct(Product product)
        {
            _applicationDbContext.Products.Add(product);
            await _applicationDbContext.SaveChangesAsync();
        }
        public async Task UpdateProduct(Product product)
        {
            _applicationDbContext.Products.Update(product);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _applicationDbContext.Products.FindAsync(id);

            if (product != null)
            {
                _applicationDbContext.Products.Remove(product);
                await _applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
