using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mottrist.Domain.Entities;

namespace Mottrist.Repository.EntityFrameworkCore.Configurations.DriversSchemaConfiguration
{
    internal class DriverInteractionConfiguration : IEntityTypeConfiguration<DriverInteraction>
    {
        public void Configure(EntityTypeBuilder<DriverInteraction> builder)
        {
            builder.HasKey(dcc => dcc.Id);

            builder.Property(x => x.IsLiked)
                    .IsRequired(false);

            builder.Property(x => x.ViewsCount)
                    .IsRequired()
                    .HasDefaultValue(0);

            builder.HasOne(di => di.Driver)
                    .WithMany(d => d.DriverInteractions)
                    .HasForeignKey(di => di.DriverId)
                    .OnDelete(DeleteBehavior.Restrict);

            // Relationships
            builder.HasOne(di => di.User)
                    .WithMany(u => u.DriverInteractions)
                    .HasForeignKey(di => di.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("DriverInteractions", schema: "Drivers");
        }
    }

}
