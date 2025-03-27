using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mottrist.Domain.Entities.CarDetails;

namespace Mottrist.Repository.EntityFrameworkCore.Configurations.VehiclesSchemaConfiguration
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                   .IsRequired();

            builder.ToTable("Brands", schema: "Vehicles");

            // Seed data
            builder.HasData(
                new Brand { Id = 1, Name = "Toyota" },
                new Brand { Id = 2, Name = "Ford" },
                new Brand { Id = 3, Name = "Honda" },
                new Brand { Id = 4, Name = "Tesla" },
                new Brand { Id = 5, Name = "BMW" }
            );
        }
    }
}