using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mottrist.Domain.Entities.CarDetails;

namespace Mottrist.Repository.EntityFrameworkCore.Configurations.VehiclesSchemaConfiguration
{
    public class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(50);
            builder.Property(c => c.Id)
       .ValueGeneratedOnAdd(); // Manual assignment of Ids

            // Seed data
            builder.HasData(
                new Color { Id = 1, Name = "Red" },
                new Color { Id = 2, Name = "Blue" },
                new Color { Id = 3, Name = "Black" },
                new Color { Id = 4, Name = "White" },
                new Color { Id = 5, Name = "Green" }
            );
            builder.ToTable("Colors", schema: "Vehicles");
        }
    }
}