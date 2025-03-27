using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mottrist.Domain.LookupEntities;

namespace Mottrist.Repository.EntityFrameworkCore.Configurations.LocalizationSchemaConfiguration
{
    public class DriverLanguageConfiguration : IEntityTypeConfiguration<DriverLanguage>
    {
        public void Configure(EntityTypeBuilder<DriverLanguage> builder)
        {
            builder.HasKey(dcc => new { dcc.DriverId, dcc.LanguageId });

            builder.HasOne(x => x.Driver)
                .WithMany(x => x.DriverLanguages)
                .IsRequired()
                .HasForeignKey(x => x.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Language)
                .WithMany(x=> x.DriverLanguages)
                .IsRequired()
                .HasForeignKey(x => x.LanguageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("DriverLanguages", schema: "Localization");
        }
    }
}