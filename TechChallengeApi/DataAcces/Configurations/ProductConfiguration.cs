using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAcces.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Type)
                   .WithMany(x => x.Products)
                   .HasPrincipalKey(x => x.Id)
                   .HasForeignKey(x => x.TypeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
