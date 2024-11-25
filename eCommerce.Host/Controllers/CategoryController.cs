using eCommerce.Application.DTOs.Category;
using eCommerce.Application.DTOs.product;
using eCommerce.Application.Services.Implementaions;
using eCommerce.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await categoryService.GetAllAsync();
            return categories.Any() ? Ok(categories) : NotFound(categories);
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var category = await categoryService.GetByIdAsync(id);
            return category != null ? Ok(category) : NotFound(category);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreateCategory category)
        {
            if(!ModelState.IsValid)return BadRequest(ModelState);
            var result = await categoryService.AddAsync(category);
            return result.Success ? Ok(result) : BadRequest(result);

        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateCategory category)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await categoryService.UpdateAsync(category);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await categoryService.DeleteAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
