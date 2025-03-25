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
                .HasMaxLength(20);

            builder.Property(d => d.NationailtyId)
                .IsRequired();

            builder.Property(d => d.UserId)
                .IsRequired();

            builder.Property(d => d.CarId)
                .IsRequired(false); 

            builder.HasOne(d => d.Car)
                .WithOne()
                .HasForeignKey<Driver>(d => d.CarId)
                .OnDelete(DeleteBehavior.Restrict);


            // Relation with User
            builder.HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation with Country
            builder.HasOne(d => d.Country)
                .WithMany()
                .HasForeignKey(d => d.NationailtyId)
                .OnDelete(DeleteBehavior.Restrict);

            // Adding Indexes for optimized querying
            builder.HasIndex(d => d.NationailtyId);
            builder.HasIndex(d => d.UserId);

            builder.ToTable("Drivers", schema: "Drivers");
        }
    }
}
