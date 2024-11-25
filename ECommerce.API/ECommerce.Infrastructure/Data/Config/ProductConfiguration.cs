using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ECommerce.Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.imageUrl);
            builder.Property(p => p.Price).HasColumnType("decimal");
            builder.HasOne(p => p.productBrand).WithMany().HasForeignKey(p => p.productBrandId);
            builder.HasOne(p => p.productType).WithMany().HasForeignKey(p => p.productTypeId);
        }
    }
}
