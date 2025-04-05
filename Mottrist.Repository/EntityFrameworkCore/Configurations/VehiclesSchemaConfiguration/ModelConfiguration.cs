using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mottrist.Domain.Entities.CarDetails;

namespace Mottrist.Repository.EntityFrameworkCore.Configurations.VehiclesSchemaConfiguration
{
    public class ModelConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name)
                   .IsRequired();

            builder.ToTable("Models", schema: "Vehicles");

            // Seed data
            builder.HasData(
     new Model { Id = 1, Name = "Corolla" },
     new Model { Id = 2, Name = "Mustang" },
     new Model { Id = 3, Name = "Civic" },
     new Model { Id = 4, Name = "Model S" },
     new Model { Id = 5, Name = "X5" },
     new Model { Id = 6, Name = "F-150" },
     new Model { Id = 7, Name = "Accord" },
     new Model { Id = 8, Name = "A4" },
     new Model { Id = 9, Name = "Camry" },
     new Model { Id = 10, Name = "Q5" },
     new Model { Id = 11, Name = "RX" },
     new Model { Id = 12, Name = "Tucson" },
     new Model { Id = 13, Name = "Explorer" },
     new Model { Id = 14, Name = "Kona" },
     new Model { Id = 15, Name = "911" },
     new Model { Id = 16, Name = "Grand Cherokee" },
     new Model { Id = 17, Name = "Range Rover" },
     new Model { Id = 18, Name = "Charger" },
     new Model { Id = 19, Name = "Cherokee" },
     new Model { Id = 20, Name = "X6" }
 );

        }
    }
}