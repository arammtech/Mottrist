using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mottrist.Domain.Entities.CarDetails;

namespace Mottrist.Repository.EntityFrameworkCore.Configurations.VehiclesSchemaConfiguration
{
    public class BodyTypeConfiguration : IEntityTypeConfiguration<BodyType>
    {
        public void Configure(EntityTypeBuilder<BodyType> builder)
        {
            builder.HasKey(bt => bt.Id);

            builder.Property(bt => bt.Type)
                   .IsRequired();

            builder.ToTable("BodyTypes", schema: "Vehicles");

            // Seed data
            builder.HasData(
      new BodyType { Id = 1, Type = "Sedan" },
      new BodyType { Id = 2, Type = "SUV" },
      new BodyType { Id = 3, Type = "Hatchback" },
      new BodyType { Id = 4, Type = "Coupe" },
      new BodyType { Id = 5, Type = "Pickup" },
      new BodyType { Id = 6, Type = "Convertible" },
      new BodyType { Id = 7, Type = "Wagon" },
      new BodyType { Id = 8, Type = "Minivan" },
      new BodyType { Id = 9, Type = "Roadster" },
      new BodyType { Id = 10, Type = "Crossover" },
      new BodyType { Id = 11, Type = "Limousine" },
      new BodyType { Id = 12, Type = "Van" },
      new BodyType { Id = 13, Type = "Sports Car" },
      new BodyType { Id = 14, Type = "Luxury Sedan" },
      new BodyType { Id = 15, Type = "Coupe Convertible" },
      new BodyType { Id = 16, Type = "Station Wagon" },
      new BodyType { Id = 17, Type = "Supercar" },
      new BodyType { Id = 18, Type = "Hypercar" },
      new BodyType { Id = 19, Type = "Off-road" },
      new BodyType { Id = 20, Type = "Targa" }
  );

        }
    }
}