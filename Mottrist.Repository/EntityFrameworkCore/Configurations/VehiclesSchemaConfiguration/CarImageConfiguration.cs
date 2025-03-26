using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mottrist.Domain.Entities.CarDetails;

namespace Mottrist.Repository.EntityFrameworkCore.Configurations.VehiclesSchemaConfiguration
{
    public class CarImageConfiguration : IEntityTypeConfiguration<CarImage>
    {
        public void Configure(EntityTypeBuilder<CarImage> builder)
        {
            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.ImageUrl)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.HasOne(ci => ci.Car)
                   .WithMany()
                   .HasForeignKey(ci => ci.CarId);
            builder.HasIndex(ci => ci.CarId);

            builder.ToTable("CarImages", schema: "Vehicles");

        }
    }
}