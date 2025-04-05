using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mottrist.Domain.LookupEntities;

namespace Mottrist.Repository.EntityFrameworkCore.Configurations.LocalizationSchemaConfiguration
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.ToTable("Languages", schema: "Localization");
            builder.HasData(
                new Language { Id = 1, Name = "English" },
                new Language { Id = 2, Name = "Arabic" },
                new Language { Id = 3, Name = "French" },
                new Language { Id = 4, Name = "Spanish" }
            );
        }
    } 
}