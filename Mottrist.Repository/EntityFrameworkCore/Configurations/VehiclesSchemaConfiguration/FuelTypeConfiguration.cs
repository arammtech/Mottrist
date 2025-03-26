using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mottrist.Domain.Entities.CarDetails;

namespace Mottrist.Repository.EntityFrameworkCore.Configurations.VehiclesSchemaConfiguration
{
    public class FuelTypeConfiguration : IEntityTypeConfiguration<FuelType>
    {
        public void Configure(EntityTypeBuilder<FuelType> builder)
        {
            builder.HasKey(ft => ft.Id);

            builder.Property(ft => ft.Type)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.ToTable("FuelTypes", schema: "Vehicles");

            // Seed data
            builder.HasData(
                new FuelType { Id = 1, Type = "Petrol" },
                new FuelType { Id = 2, Type = "Diesel" },
                new FuelType { Id = 3, Type = "Electric" },
                new FuelType { Id = 4, Type = "Hybrid" }
            );

        }
    }
}