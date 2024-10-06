using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAcces.Configurations
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasMany(x => x.Products)
                   .WithOne(x => x.Type)
                   .HasPrincipalKey(x => x.Id)
                   .HasForeignKey(x => x.TypeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
