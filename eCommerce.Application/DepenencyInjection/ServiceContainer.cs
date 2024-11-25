
using eCommerce.Application.Mapping;
using eCommerce.Application.Services.Implementaions;
using eCommerce.Application.Services.Implementaions.Authentication;
using eCommerce.Application.Services.Interfaces.Authentication;
using eCommerce.Application.Validation.Authentication;
using eCommerce.Domain.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Application.DepenencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingConfiq));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateUserValidation>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            return services;
        }
    }
}
