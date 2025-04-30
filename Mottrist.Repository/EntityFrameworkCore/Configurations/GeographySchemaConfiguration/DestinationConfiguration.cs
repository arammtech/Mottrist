using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mottrist.Domain.Entities;

namespace Mottrist.Repository.EntityFrameworkCore.Configurations.GeographySchemaConfiguration
{
    internal class DestinationConfiguration : IEntityTypeConfiguration<Destination>
    {
        public void Configure(EntityTypeBuilder<Destination> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(c => c.Type)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(c => c.Description)
                   .IsRequired(false);

            builder.Property(c => c.ImageUrl)
                   .IsRequired();

            builder.Property(c => c.CityId)
                   .IsRequired();

            builder.HasOne(c => c.City)
                   .WithMany(city => city.Destinations)
                   .HasForeignKey(c => c.CityId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Destinations", schema: "Geography");

        }
    }
}
