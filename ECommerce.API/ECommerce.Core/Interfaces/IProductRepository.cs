using ECommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductById(int id);
        Task<List<Product>> GetAllProducts();
        Task<List<ProductBrand>> GetAllProductBrands();
        Task<List<ProductType>> GetAllProductTypes();
    }
}
