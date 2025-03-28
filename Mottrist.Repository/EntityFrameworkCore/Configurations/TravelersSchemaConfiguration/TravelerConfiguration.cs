using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mottrist.Domain.Entities;

namespace Mottrist.Repository.EntityFrameworkCore.Configurations.TravellersSchemaConfiguration
{
    public class TravelerConfiguration : IEntityTypeConfiguration<Traveler>
    {
        public void Configure(EntityTypeBuilder<Traveler> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.WhatsAppNumber)
                .HasMaxLength(20);

            builder.Property(t => t.NationalityId)
                .IsRequired();

            builder.Property(t => t.UserId)
                .IsRequired();

            builder.HasOne(b => b.Country)
                .WithMany()
                .HasForeignKey(b => b.NationalityId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(builder => builder.User)
                .WithOne()
                .HasForeignKey<Traveler>(t => t.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Travellers", schema: "Travellers");
        }
    }
}
