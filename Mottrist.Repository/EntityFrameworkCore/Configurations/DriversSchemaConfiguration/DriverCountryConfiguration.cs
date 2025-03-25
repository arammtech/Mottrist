using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mottrist.Domain.Enums;
using Mottrist.Domain.LookupEntities;

namespace Mottrist.Repository.EntityFrameworkCore.Configurations.DriversSchemaConfiguration
{
    public class DriverCountryConfiguration : IEntityTypeConfiguration<DriverCountry>
    {
        public void Configure(EntityTypeBuilder<DriverCountry> builder)
        {
            builder.HasKey(dcc => new { dcc.DriverId, dcc.CountryId});

            builder.HasOne(dcc => dcc.Driver) 
                   .WithMany(d => d.DriverCountrites)
                   .HasForeignKey(dcc => dcc.DriverId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(dcc => dcc.Country) 
                   .WithMany(c => c.DriverCountryCoverages)
                   .HasForeignKey(dcc => dcc.CountryId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Property(dcc => dcc.WorkStatus)
                .IsRequired()
                .HasConversion(
                    builder => (byte)builder,
                    builder => (WorkStatus)builder)
                .HasColumnType("tinyint");

            builder.ToTable("DriverCountryCoverages", schema: "Drivers");
        }
    }
}
