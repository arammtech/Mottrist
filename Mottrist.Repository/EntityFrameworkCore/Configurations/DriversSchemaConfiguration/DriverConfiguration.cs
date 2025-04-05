using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Enums;

namespace Mottrist.Repository.EntityFrameworkCore.Configurations.DriversSchemaConfiguration
{
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.WhatsAppNumber)
                .HasMaxLength(20);

            builder.Property(d => d.NationalityId)
                .IsRequired();

            builder.Property(d => d.UserId)
                .IsRequired();

            builder.Property(d => d.CarId)
                .IsRequired(false); 

            builder.Property(d => d.Status)
                .HasConversion
                (
                    builder => (byte)builder,
                    builder => (DriverStatus)builder)
                .HasDefaultValue(DriverStatus.Binding);

            builder.HasOne(d => d.Car)
                .WithOne()
                .HasForeignKey<Driver>(d => d.CarId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation with User
            builder.HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Relation with Country
            builder.HasOne(d => d.Country)
                .WithMany(x => x.Drivers)
                .HasForeignKey(d => d.NationalityId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Adding Indexes for optimized querying
            builder.HasIndex(d => d.UserId);

            builder.ToTable("Drivers", schema: "Drivers");
        }
    }
}
