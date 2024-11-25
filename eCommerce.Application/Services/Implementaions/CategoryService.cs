using AutoMapper;
using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Category;
using eCommerce.Application.DTOs.product;
using eCommerce.Application.DTOs.Product;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces;
using eCommerce.Domain.Services;
namespace eCommerce.Application.Services.Implementaions
{
    public class CategoryService(IGeneric<Category> categoryInterface, IMapper mapper) : ICategoryService
    {
        public async Task<ServiceResponse> AddAsync(CreateCategory category)
        {
            var mapData = mapper.Map<Category>(category);
            int result = await categoryInterface.AddAsync(mapData);
            if (result > 0)
                return new ServiceResponse(true, "Category Added");
            return new ServiceResponse(false, "Category failed to be Added");
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            int result = await categoryInterface.DeleteAsync(id);            
            if (result > 0)
                return new ServiceResponse(true, "category deleted");
            return new ServiceResponse(false, "Category not found or failed to be deleted");
        }

        public async Task<IEnumerable<GetCategory>> GetAllAsync()
        {
            var categories = await categoryInterface.GetAllAsync();
            if (!categories.Any()) return [];
            return mapper.Map<IEnumerable<GetCategory>>(categories);
        }

        public async Task<GetCategory> GetByIdAsync(Guid id)
        {
            var category = await categoryInterface.GetByIdAsync(id);
            if (category == null) return new GetCategory();
            return mapper.Map<GetCategory>(category);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateCategory category)
        {
            var mapData = mapper.Map<Category>(category);
            int result = await categoryInterface.UpdateAsync(mapData);
            if (result > 0)
                return new ServiceResponse(true, "Category Updated");
            return new ServiceResponse(false, "Category failed to be Updated");
        }
    }
}

