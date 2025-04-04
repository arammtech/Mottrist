﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mottrist.Domain.Entities.CarDetails;

namespace Mottrist.Repository.EntityFrameworkCore.Configurations.VehiclesSchemaConfiguration
{
    public class ModelConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name)
                   .IsRequired();

            builder.ToTable("Models", schema: "Vehicles");

            // Seed data
            builder.HasData(
                new Model { Id = 1, Name = "Corolla" },
                new Model { Id = 2, Name = "Mustang" },
                new Model { Id = 3, Name = "Civic" },
                new Model { Id = 4, Name = "Model S" },
                new Model { Id = 5, Name = "X5" }
            );
        }
    }
}