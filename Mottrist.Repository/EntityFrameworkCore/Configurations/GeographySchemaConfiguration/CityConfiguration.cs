using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mottrist.Domain.LookupEntities;

namespace Mottrist.Repository.EntityFrameworkCore.Configurations.GeographySchemaConfiguration
{
    internal class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name) 
                   .IsRequired()
                   .HasMaxLength(200);

            builder.HasOne(c => c.Country)
                   .WithMany(country => country.Cities)
                   .HasForeignKey(c => c.CountryId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Cities", schema: "Geography");
        }
    } 
}
