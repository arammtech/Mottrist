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
            builder.HasData(GetFuelTypes());
        }

        private List<FuelType> GetFuelTypes()
        {
            return new List<FuelType>
    {
        // Fossil Fuels
        new FuelType { Id = 1, Type = "Petrol (Gasoline)" },
        new FuelType { Id = 2, Type = "Diesel" },
        new FuelType { Id = 3, Type = "Liquefied Petroleum Gas (LPG)" },
        new FuelType { Id = 4, Type = "Compressed Natural Gas (CNG)" },
        new FuelType { Id = 5, Type = "Natural Gas" }, //broader category
        new FuelType { Id = 6, Type = "Kerosene" },
        new FuelType { Id = 7, Type = "Heavy Fuel Oil (HFO)" },

        // Biofuels
        new FuelType { Id = 8, Type = "Ethanol (E85, etc.)" },
        new FuelType { Id = 9, Type = "Biodiesel (B20, etc.)" },
        new FuelType { Id = 10, Type = "Biobutanol" },
        new FuelType { Id = 11, Type = "Biogas" },

        // Electricity
        new FuelType { Id = 12, Type = "Electricity" },

        // Hydrogen
        new FuelType { Id = 13, Type = "Hydrogen" },
        new FuelType{Id=14, Type = "Green Hydrogen"},

        //Synthetic Fuels
        new FuelType {Id = 15, Type = "Synthetic Diesel"},
        new FuelType {Id = 16, Type = "Synthetic Gasoline"},
        new FuelType{Id=17, Type = "E-Fuel"},

        //Aviation Fuels
        new FuelType {Id = 18, Type = "Jet Fuel (Jet A, Jet A-1)"},
        new FuelType{Id=19, Type = "Avgas"}, //aviation gasoline

        // Other
        new FuelType { Id = 20, Type = "Propane" },
        new FuelType { Id = 21, Type = "Methanol" },
    };
        }
    }
}