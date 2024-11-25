﻿using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.product;
using eCommerce.Application.DTOs.Product;
using eCommerce.Domain.Entities;

namespace eCommerce.Domain.Services
{
    public interface IProductService
    {
        Task<IEnumerable<GetProduct>> GetAllAsync();
        Task<GetProduct> GetByIdAsync(Guid id);
        Task<ServiceResponse> AddAsync(CreateProduct product);
        Task<ServiceResponse> UpdateAsync(UpdateProduct product);
        Task<ServiceResponse> DeleteAsync(Guid id);
    }
}
