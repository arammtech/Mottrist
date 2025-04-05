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
      new Brand { Id = 5, Name = "BMW" },
      new Brand { Id = 6, Name = "Chevrolet" },
      new Brand { Id = 7, Name = "Mercedes-Benz" },
      new Brand { Id = 8, Name = "Audi" },
      new Brand { Id = 9, Name = "Nissan" },
      new Brand { Id = 10, Name = "Volkswagen" },
      new Brand { Id = 11, Name = "Hyundai" },
      new Brand { Id = 12, Name = "Kia" },
      new Brand { Id = 13, Name = "Subaru" },
      new Brand { Id = 14, Name = "Mazda" },
      new Brand { Id = 15, Name = "Lexus" },
      new Brand { Id = 16, Name = "Jaguar" },
      new Brand { Id = 17, Name = "Porsche" },
      new Brand { Id = 18, Name = "Land Rover" },
      new Brand { Id = 19, Name = "Ferrari" },
      new Brand { Id = 20, Name = "Lamborghini" }
  );

        }
    }
}