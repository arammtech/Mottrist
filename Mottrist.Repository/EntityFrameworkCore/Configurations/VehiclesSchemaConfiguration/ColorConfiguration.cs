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

            builder.ToTable("Colors", schema: "Vehicles");

            // Seed data
            builder.HasData(
      new Color { Id = 1, Name = "Red" },
      new Color { Id = 2, Name = "Blue" },
      new Color { Id = 3, Name = "Black" },
      new Color { Id = 4, Name = "White" },
      new Color { Id = 5, Name = "Green" },
      new Color { Id = 6, Name = "Yellow" },
      new Color { Id = 7, Name = "Orange" },
      new Color { Id = 8, Name = "Purple" },
      new Color { Id = 9, Name = "Silver" },
      new Color { Id = 10, Name = "Gray" },
      new Color { Id = 11, Name = "Brown" },
      new Color { Id = 12, Name = "Beige" },
      new Color { Id = 13, Name = "Pink" },
      new Color { Id = 14, Name = "Gold" },
      new Color { Id = 15, Name = "Turquoise" },
      new Color { Id = 16, Name = "Teal" },
      new Color { Id = 17, Name = "Magenta" },
      new Color { Id = 18, Name = "Copper" },
      new Color { Id = 19, Name = "Ivory" },
      new Color { Id = 20, Name = "Champagne" }
  );

        }
    }
}