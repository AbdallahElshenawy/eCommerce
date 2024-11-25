using eCommerce.Application.DTOs.Category;
using eCommerce.Application.DTOs.product;
namespace eCommerce.Application.DTOs.Product
{
    public class GetProduct : ProductBase
    {
        public Guid Id { get; set; }
        public GetCategory? Category { get; set; } 

    }
}
