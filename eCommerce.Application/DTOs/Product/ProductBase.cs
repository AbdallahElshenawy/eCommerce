using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.DTOs.product
{
    public class ProductBase
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public string? Image { get; set; }
        
        [Required]
        public int Quantity { get; set; }
        
        [Required]
        public Guid CategoryId { get; set; }
    }
}
