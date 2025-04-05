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
            builder.HasKey(dcc => dcc.Id);

            builder.Property(dcc => dcc.WorkStatus)
                .IsRequired()
                .HasConversion(
                    builder => (byte)builder,
                    builder => (WorkStatus)builder);

            builder.HasOne(dcc => dcc.Driver) 
                .WithMany(d => d.DriverCountries)
                .IsRequired()
                .HasForeignKey(dcc => dcc.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(dcc => dcc.Country) 
                   .WithMany(c => c.DriverCountries)
                   .IsRequired()
                   .HasForeignKey(dcc => dcc.CountryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("DriverCountries", schema: "Drivers");
        }
    }
}
