using AutoMapper;
using eCommerce.Application.DTOs.Card;
using eCommerce.Application.DTOs.Category;
using eCommerce.Application.DTOs.Identity;
using eCommerce.Application.DTOs.product;
using eCommerce.Application.DTOs.Product;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Entities.Cart;
using eCommerce.Domain.Entities.Identity;
namespace eCommerce.Application.Mapping
{
    public class MappingConfiq : Profile
    {
        public MappingConfiq()
        {
            CreateMap<CreateCategory, Category>();
            CreateMap<CreateProduct, Product>();

            CreateMap<Category, GetCategory>();
            CreateMap<Product, GetProduct>();
            CreateMap<CreateUser,AppUser>();
            CreateMap<LoginUser,AppUser>();
            CreateMap<PaymentMethod,GetPaymentMethod>();
            CreateMap<CreateAchieve, Achieve>();
        }
    }
}
