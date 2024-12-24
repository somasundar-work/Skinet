using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skinet.Entities.Product;

namespace Skinet.Data.Config;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    /// <summary>
    /// Configures the properties of the <see cref="Product"/> entity.
    /// </summary>
    /// <param name="builder">The builder used to configure the <see cref="Product"/> entity.</param>
    /// <remarks>
    /// The following configurations are applied:
    /// <list type="bullet">
    /// <item><description><see cref="Product.Id"/> is required.</description></item>
    /// <item><description><see cref="Product.Name"/> is required and has a maximum length of 100 characters.</description></item>
    /// <item><description><see cref="Product.Description"/> is required and has a maximum length of 500 characters.</description></item>
    /// <item><description><see cref="Product.Price"/> is of type decimal with a precision of 18 and scale of 2.</description></item>
    /// <item><description><see cref="Product.PictureUrl"/> has a maximum length of 100 characters.</description></item>
    /// <item><description><see cref="Product.Brand"/> is required and has a maximum length of 100 characters.</description></item>
    /// <item><description><see cref="Product.Category"/> is required and has a maximum length of 100 characters.</description></item>
    /// </list>
    /// </remarks>
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Description).IsRequired().HasMaxLength(500);
        builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
        builder.Property(p => p.PictureUrl).HasMaxLength(100);
        builder.Property(p => p.Brand).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Category).IsRequired().HasMaxLength(100);
    }
}
