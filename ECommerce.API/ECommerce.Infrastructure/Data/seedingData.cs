using ECommerce.Core.Entities;

using System.Text.Json;

namespace ECommerce.Infrastructure.Data
{
    public class seedingData
    {
        public static async Task Seed(ECommerceDbContext context)
        {

            if (!context.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText(@"F:\eCommerce\ECommerce.API\ECommerce.Infrastructure\Data\seedData\brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                context.ProductBrands.AddRange(brands);
            }
            if (!context.ProductTypes.Any())
            {
                var typesData = File.ReadAllText("F:\\eCommerce\\ECommerce.API\\ECommerce.Infrastructure\\Data\\seedData\\types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                context.ProductTypes.AddRange(types);
            }
            if (!context.Products.Any())
            {
                var productsData = File.ReadAllText("F:\\eCommerce\\ECommerce.API\\ECommerce.Infrastructure\\Data\\seedData\\products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                context.Products.AddRange(products);
            }
            if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();

        }
    }
}
