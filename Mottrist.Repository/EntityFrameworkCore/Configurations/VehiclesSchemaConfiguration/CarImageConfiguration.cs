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
                   .IsRequired();

            builder.HasOne(ci => ci.Car)
                   .WithMany()
                   .IsRequired()
                   .HasForeignKey(ci => ci.CarId);

            builder.ToTable("CarImages", schema: "Vehicles");
        }
    }
}