using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.DTOs.product
{
    public class UpdateProduct: ProductBase
    {
        [Required]
        public Guid Id { get; set; }

        
    }
}
