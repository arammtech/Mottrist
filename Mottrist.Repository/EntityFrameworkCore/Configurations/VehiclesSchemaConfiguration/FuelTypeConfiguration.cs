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
        new FuelType { Id = 4, Type = "Hybrid" },
        new FuelType { Id = 5, Type = "CNG" },
        new FuelType { Id = 6, Type = "LPG" },
        new FuelType { Id = 7, Type = "Ethanol" },
        new FuelType { Id = 8, Type = "Biofuel" },
        new FuelType { Id = 9, Type = "Hydrogen" },
        new FuelType { Id = 10, Type = "Propane" },
        new FuelType { Id = 11, Type = "Methanol" },
        new FuelType { Id = 12, Type = "Butanol" },
        new FuelType { Id = 13, Type = "Natural Gas" },
        new FuelType { Id = 14, Type = "Biodiesel" },
        new FuelType { Id = 15, Type = "Alcohol" },
        new FuelType { Id = 16, Type = "Fischer-Tropsch" },
        new FuelType { Id = 17, Type = "Electric + Petrol" },
        new FuelType { Id = 18, Type = "Electric + Diesel" },
        new FuelType { Id = 19, Type = "Electric + Hydrogen" },
        new FuelType { Id = 20, Type = "Compressed Natural Gas (CNG)" }
    );


        }
    }
}