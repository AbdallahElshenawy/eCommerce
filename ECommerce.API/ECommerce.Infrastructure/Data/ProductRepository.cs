using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Data
{
    public class ProductRepository(ECommerceDbContext context) : IProductRepository
    {
        public async Task<List<Product>> GetAllProducts()
        {
            return await context.Products.Include(p=>p.productBrand).Include(p=>p.productType).ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            var product= await context.Products.Include(p => p.productBrand).Include(p => p.productType).FirstOrDefaultAsync(p=>p.Id==id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }
            return product;
        }
        public async Task<List<ProductBrand>> GetAllProductBrands()
        {
            return await context.ProductBrands.ToListAsync();
        }
        public async Task<List<ProductType>> GetAllProductTypes()
        {
            return await context.ProductTypes.ToListAsync();
        }
    }
}
