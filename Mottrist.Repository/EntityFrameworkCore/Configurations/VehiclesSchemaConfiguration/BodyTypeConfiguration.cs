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
                   .IsRequired()
                   .HasMaxLength(50);
            builder.Property(bt => bt.Id)
       .ValueGeneratedOnAdd(); // Manual assignment of Ids

            // Seed data
            builder.HasData(
                new BodyType { Id = 1, Type = "Sedan" },
                new BodyType { Id = 2, Type = "SUV" },
                new BodyType { Id = 3, Type = "Hatchback" },
                new BodyType { Id = 4, Type = "Coupe" },
                new BodyType { Id = 5, Type = "Pickup" }
            );
            builder.ToTable("BodyTypes", schema: "Vehicles");
        }
    }
}