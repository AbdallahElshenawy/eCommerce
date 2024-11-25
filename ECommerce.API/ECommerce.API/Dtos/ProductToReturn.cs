using ECommerce.Core.Entities;

namespace ECommerce.API.Dtos
{
    public class ProductToReturn
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string? imageUrl { get; set; }

        public string productType { get; set; }

        public string productBrand { get; set; }
    }
}
