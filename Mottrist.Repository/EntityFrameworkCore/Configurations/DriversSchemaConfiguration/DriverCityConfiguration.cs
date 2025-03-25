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
            builder.HasKey(dcc => new { dcc.DriverId, dcc.CityId});

            // Relationships
            builder.HasOne(dcc => dcc.Driver)
                   .WithMany(d => d. DriverCities)
                   .HasForeignKey(dcc => dcc.DriverId)
                    .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(dcc => dcc.City)
                   .WithMany(c => c.DriverCities)
                   .HasForeignKey(dcc => dcc.CityId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);


            builder.Property(dcc => dcc.WorkStatus)
                     .IsRequired()
                     .HasConversion(
                        builder => (byte)builder,
                        builder => (WorkStatus)builder)
                     .HasColumnType("tinyint");



            builder.ToTable("DriverCityCoverages", schema: "Drivers");
        }
    }
}
