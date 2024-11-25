using AutoMapper;
using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.product;
using eCommerce.Application.DTOs.Product;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces;
using eCommerce.Domain.Services;
namespace eCommerce.Application.Services.Implementaions
{
    public class ProductService(IGeneric<Product> ProductInterface, IMapper mapper) : IProductService
    {
        public async Task<ServiceResponse> AddAsync(CreateProduct product)
        {
            var mapData = mapper.Map<Product>(product);
            int result = await ProductInterface.AddAsync(mapData);
            if (result > 0)
                return new ServiceResponse(true, "Product Added");
            return new ServiceResponse(false, "Product failed to be Added");
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            int result = await ProductInterface.DeleteAsync(id);
            
            if (result > 0)
                return new ServiceResponse(true, "Product deleted");
            return new ServiceResponse(false, "Product not found or failed to be deleted");
        }

        public async Task<IEnumerable<GetProduct>> GetAllAsync()
        {
            var products = await ProductInterface.GetAllAsync();
            if(!products.Any()) return [];
            return mapper.Map<IEnumerable<GetProduct>>(products);
        }
       

        public async Task<GetProduct> GetByIdAsync(Guid id)
        {
            var product = await ProductInterface.GetByIdAsync(id);
            if (product==null) return new GetProduct();
            return mapper.Map<GetProduct>(product);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateProduct product)
        {
            var mapData = mapper.Map<Product>(product);
            int result = await ProductInterface.UpdateAsync(mapData);
            if (result > 0)
                return new ServiceResponse(true, "Product Updated");
            return new ServiceResponse(false, "Product failed to be Updated");
        }

       
    }
}
