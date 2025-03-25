using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mottrist.Domain.Entities.CarDetails;

namespace Mottrist.Repository.EntityFrameworkCore.Configurations.CarDetailsSchemaConfiguration
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable(r => r.HasCheckConstraint("CK_Car_Year", $"[Year] >= 1900 AND [Year] <= {DateTime.Now.Year}"));

            builder.Property(c => c.Year)
                   .IsRequired()
                   .HasColumnType("int");

            builder.Property(c => c.NumberOfSeats)
                   .IsRequired()
                   .HasColumnType("tinyint");

            // Relationships
            builder.HasOne(c => c.Brand)
                   .WithMany()
                   .HasForeignKey(c => c.BrandId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Model)
                   .WithMany()
                   .HasForeignKey(c => c.ModelId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.BodyType)
                   .WithMany()
                   .HasForeignKey(c => c.BodyTypeId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.FuelType)
                   .WithMany()
                   .HasForeignKey(c => c.FuelTypeId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Color)
            .WithMany()
            .HasForeignKey(c => c.ColorId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);



            // Table mapping
            builder.ToTable("Cars", schema: "Vehicles");
        }
    }
}
