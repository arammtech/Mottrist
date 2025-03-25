using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mottrist.Domain.LookupEntities;

namespace Mottrist.Repository.EntityFrameworkCore.Configurations.DriversSchemaConfiguration
{
    public class DriverCountryCoverageConfiguration : IEntityTypeConfiguration<DriverCountryCoverage>
    {
        public void Configure(EntityTypeBuilder<DriverCountryCoverage> builder)
        {
            builder.HasKey(dcc => new { dcc.DriverId, dcc.CountryId });

            builder.HasOne(dcc => dcc.Driver) 
                   .WithMany(d => d.DriverCountryCoverages)
                   .HasForeignKey(dcc => dcc.DriverId);

            builder.HasOne(dcc => dcc.Country) 
                   .WithMany(c => c.DriverCountryCoverages)
                   .HasForeignKey(dcc => dcc.CountryId);

            builder.HasOne(dcc => dcc.CoverageType)
                   .WithMany()
                   .HasForeignKey(dcc => dcc.CoverageTypeId);

            builder.ToTable("DriverCountryCoverages", schema: "Drivers");
        }
    }
}
