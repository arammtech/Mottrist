using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mottrist.Domain.Entities;

namespace Mottrist.Repository.EntityFrameworkCore.Configurations.DriversSchemaConfiguration
{
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.WhatsAppNumber)
                .HasMaxLength(20)
                .IsRequired();

            builder.HasOne(d => d.Car)
                .WithOne()
                .HasForeignKey<Driver>(d => d.CarId);

            builder.ToTable("Drivers", schema: "Drivers");

        }
    }
}