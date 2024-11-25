using eCommerce.Application.DTOs.product;
using eCommerce.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var products = await productService.GetAllAsync();
            return products.Any() ? Ok(products) : NotFound(products);
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await productService.GetByIdAsync(id);
            return product != null ? Ok(product) : NotFound(product);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreateProduct product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await productService.AddAsync(product);
            return result.Success ? Ok(result) : BadRequest(result);

        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateProduct product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await productService.UpdateAsync(product);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await productService.DeleteAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
