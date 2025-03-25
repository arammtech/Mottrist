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

            builder.ToTable("BodyTypes", schema: "Vehicles");
        }
    }
}