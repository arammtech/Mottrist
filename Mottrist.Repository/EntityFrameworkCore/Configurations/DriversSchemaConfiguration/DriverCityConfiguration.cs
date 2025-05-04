using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mottrist.Domain.Enums;
using Mottrist.Domain.LookupEntities;

namespace Mottrist.Repository.EntityFrameworkCore.Configurations.DriversSchemaConfiguration
{
    internal class DriverCityConfiguration : IEntityTypeConfiguration<DriverCity>
    {
        public void Configure(EntityTypeBuilder<DriverCity> builder)
        {
            // Composite key for uniqueness
            builder.HasKey(dcc=> dcc.Id);


            builder.Property(dcc => dcc.WorkStatus)
                     .IsRequired()
                     .HasConversion(
                        builder => (byte)builder,
                        builder => (WorkStatus)builder)
                     .HasComment("Stores the work status of the driver in the city: WorkedOn = 1, CoverNow = 2.");

            // Relationships
            builder.HasOne(dcc => dcc.Driver)
                   .WithMany(d => d. DriverCities)
                   .HasForeignKey(dcc => dcc.DriverId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(dcc => dcc.City)
                   .WithMany(c => c.DriverCities)
                   .HasForeignKey(dcc => dcc.CityId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("DriverCities", schema: "Drivers");
        }
    }

}
