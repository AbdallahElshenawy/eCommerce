using AutoMapper;
using ECommerce.API.Dtos;
using ECommerce.Core.Entities;

namespace ECommerce.API.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturn>()
            .ForMember(d => d.productBrand, o => o.MapFrom(s => s.productBrand.Name))
            .ForMember(d => d.productType, o => o.MapFrom(s => s.productType.Name))
            .ForMember(d => d.imageUrl, o => o.MapFrom<ImageUrlResolver>());
        }
    }
}
