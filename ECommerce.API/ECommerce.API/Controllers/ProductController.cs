using AutoMapper;
using ECommerce.API.Dtos;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using ECommerce.Core.Specifications;
using ECommerce.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IRepository<Product> productRepository, IRepository<ProductType> productTypeRepo, 
        IRepository<ProductBrand> productBrandRepo, IProductRepository productRepo,IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<ProductToReturn>>> GetProducts()
        {
            var spec = new ProducctWithTypesAndBrandsSpecification();
            var products = await productRepository.GetAllSpec(spec);
            return Ok(mapper.Map<List<ProductToReturn>>(products));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturn>> GetProduct(int id)
        {
            var spec = new ProducctWithTypesAndBrandsSpecification(id);

            var product = await productRepository.GetEntityWithSpec(spec);
            return mapper.Map<ProductToReturn>(product);
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            var products = await productBrandRepo.GetAll();
            return Ok(products);
        }
        [HttpGet("Types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            var products = await productTypeRepo.GetAll();
            return Ok(products);
        }
    }
}
