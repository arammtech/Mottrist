using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mottrist.Domain.LookupEntities;

namespace Mottrist.Repository.EntityFrameworkCore.Configurations.LocalizationSchemaConfiguration
{
    public class DriverLanguageConfiguration : IEntityTypeConfiguration<DriverLanguage>
    {
        public void Configure(EntityTypeBuilder<DriverLanguage> builder)
        {
            builder.HasKey(dl => dl.Id);

            builder.ToTable("DriverLanguages", schema: "Localization");
        }
    }
}