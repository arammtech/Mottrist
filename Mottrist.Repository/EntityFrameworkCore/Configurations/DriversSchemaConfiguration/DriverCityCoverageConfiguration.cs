using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mottrist.Domain.LookupEntities;

namespace Mottrist.Repository.EntityFrameworkCore.Configurations.DriversSchemaConfiguration
{
    internal class DriverCityCoverageConfiguration : IEntityTypeConfiguration<DriverCityCoverage>
    {
        public void Configure(EntityTypeBuilder<DriverCityCoverage> builder)
        {
            builder.HasKey(dcc => new { dcc.DriverId, dcc.CityId });

            builder.HasOne(dcc => dcc.Driver) 
                   .WithMany(d => d.DriverCityCoverages)
                   .HasForeignKey(dcc => dcc.DriverId);

            builder.HasOne(dcc => dcc.City) 
                   .WithMany(c => c.DriverCityCoverages)
                   .HasForeignKey(dcc => dcc.CityId);

            builder.HasOne(dcc => dcc.CoverageType)
                   .WithMany()
                   .HasForeignKey(dcc => dcc.CoverageTypeId);

            builder.ToTable("DriverCityCoverages", schema: "Drivers");
        }
    } 
}
