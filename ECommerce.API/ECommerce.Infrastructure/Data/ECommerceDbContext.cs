
using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ECommerce.Infrastructure.Data
{
    public class ECommerceDbContext:DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options):base(options) 
        {
            
        }
        public DbSet<Product>Products { get; set; }
        public DbSet<ProductBrand>ProductBrands { get; set; }
        public DbSet<ProductType>ProductTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
