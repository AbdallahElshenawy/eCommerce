using AutoMapper;
using ECommerce.API.Dtos;
using ECommerce.Core.Entities;

namespace ECommerce.API.Helpers
{
    public class ImageUrlResolver(IConfiguration config) : IValueResolver<Product, ProductToReturn, string>
    {
        public string Resolve(Product source, ProductToReturn destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.imageUrl))
            {
                return config["ApiUrl"] + source.imageUrl;
            }
            return null;
        }
    }
}
