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

        }
    }
}