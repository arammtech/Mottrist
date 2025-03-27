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

            builder.Property(c => c.Year)
                   .IsRequired();

            builder.Property(c => c.NumberOfSeats)
                   .IsRequired();

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

            builder.HasData(
                new Car { Id = 1, BrandId = 1, Year = 2022, NumberOfSeats = 5, ModelId = 1, ColorId = 1, BodyTypeId = 1, FuelTypeId = 1 },
                new Car { Id = 2, BrandId = 2, Year = 2021, NumberOfSeats = 7, ModelId = 2, ColorId = 2, BodyTypeId = 2, FuelTypeId = 2 },
                new Car { Id = 3, BrandId = 1, Year = 2022, NumberOfSeats = 5, ModelId = 1, ColorId = 1, BodyTypeId = 1, FuelTypeId = 1 },
                new Car { Id = 4, BrandId = 2, Year = 2021, NumberOfSeats = 7, ModelId = 2, ColorId = 2, BodyTypeId = 2, FuelTypeId = 2 },
                new Car { Id = 5, BrandId = 3, Year = 2023, NumberOfSeats = 4, ModelId = 3, ColorId = 3, BodyTypeId = 3, FuelTypeId = 1 }
            );

            // Table mapping
            builder.ToTable("Cars", schema: "Vehicles");
        }
    }
}
