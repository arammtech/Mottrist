using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mottrist.Domain.Entities.CarDetails;

namespace Mottrist.Repository.EntityFrameworkCore.Configurations.VehiclesSchemaConfiguration
{
    public class CarImageConfiguration : IEntityTypeConfiguration<CarImage>
    {
        public void Configure(EntityTypeBuilder<CarImage> builder)
        {
            builder.HasNoKey();
            builder.Property(ci => ci.ImageUrl)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.ToTable("CarImages", schema: "Vehicles");

        }
    }
}