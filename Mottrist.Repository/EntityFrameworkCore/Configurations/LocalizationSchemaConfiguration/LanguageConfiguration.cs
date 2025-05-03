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

            builder.HasData(GetLanguages());
        }
        private List<Language> GetLanguages()
        {
            return new List<Language>
            {
                new Language { Id = 1, Name = "English" },
                new Language { Id = 2, Name = "Spanish" },
                new Language { Id = 3, Name = "French" },
                new Language { Id = 4, Name = "German" },
                new Language { Id = 5, Name = "Chinese" },
                new Language { Id = 6, Name = "Japanese" },
                new Language { Id = 7, Name = "Arabic" },
                new Language { Id = 8, Name = "Russian" },
                new Language { Id = 9, Name = "Portuguese" },
                new Language { Id = 10, Name = "Hindi" },
                new Language { Id = 11, Name = "Bengali" },
                new Language { Id = 12, Name = "Urdu" },
                new Language { Id = 13, Name = "Italian" },
                new Language { Id = 14, Name = "Dutch" },
                new Language { Id = 15, Name = "Greek" },
                new Language { Id = 16, Name = "Turkish" },
                new Language { Id = 17, Name = "Korean" },
                new Language { Id = 18, Name = "Vietnamese" },
                new Language { Id = 19, Name = "Swedish" },
                new Language { Id = 20, Name = "Polish" },
                new Language { Id = 21, Name = "Finnish" },
                new Language { Id = 22, Name = "Hebrew" },
                new Language { Id = 23, Name = "Malay" },
                new Language { Id = 24, Name = "Indonesian" },
                new Language { Id = 25, Name = "Thai" },
                new Language { Id = 26, Name = "Hungarian" },
                new Language { Id = 27, Name = "Czech" },
                new Language { Id = 28, Name = "Romanian" },
                new Language { Id = 29, Name = "Bulgarian" },
                new Language { Id = 30, Name = "Persian" },
                new Language { Id = 31, Name = "Swahili" },
                new Language { Id = 32, Name = "Filipino" },
                new Language { Id = 33, Name = "Tamil" },
                new Language { Id = 34, Name = "Telugu" },
                new Language { Id = 35, Name = "Marathi" },
                new Language { Id = 36, Name = "Serbian" },
                new Language { Id = 37, Name = "Croatian" },
                new Language { Id = 38, Name = "Slovak" },
                new Language { Id = 39, Name = "Danish" },
                new Language { Id = 40, Name = "Norwegian" },
                new Language { Id = 41, Name = "Ukrainian" },
                new Language { Id = 42, Name = "Lithuanian" },
                new Language { Id = 43, Name = "Latvian" },
                new Language { Id = 44, Name = "Estonian" },
                new Language { Id = 45, Name = "Macedonian" },
                new Language { Id = 46, Name = "Armenian" },
                new Language { Id = 47, Name = "Georgian" },
                new Language { Id = 48, Name = "Pashto" },
                new Language { Id = 49, Name = "Sinhala" },
                new Language { Id = 50, Name = "Mongolian" },
                new Language { Id = 51, Name = "Basque" },
                new Language { Id = 52, Name = "Catalan" },
                new Language { Id = 53, Name = "Malagasy" },
                new Language { Id = 54, Name = "Azerbaijani" },
                new Language { Id = 55, Name = "Kurdish" },
                new Language { Id = 56, Name = "Tatar" },
                new Language { Id = 57, Name = "Belarusian" },
                new Language { Id = 58, Name = "Welsh" },
                new Language { Id = 59, Name = "Irish" },
                new Language { Id = 60, Name = "Yiddish" },
                new Language { Id = 61, Name = "Nepali" },
                new Language { Id = 62, Name = "Javanese" },
                new Language { Id = 63, Name = "Sundanese" },
                new Language { Id = 64, Name = "Gujarati" },
                new Language { Id = 65, Name = "Haitian Creole" },
                new Language { Id = 66, Name = "Zulu" },
                new Language { Id = 67, Name = "Xhosa" },
                new Language { Id = 68, Name = "Hausa" },
                new Language { Id = 69, Name = "Igbo" },
                new Language { Id = 70, Name = "Samoan" },
                new Language { Id = 71, Name = "Māori" },
                new Language { Id = 72, Name = "Tibetan" },
                new Language { Id = 73, Name = "Lao" },
                new Language { Id = 74, Name = "Burmese" },
                new Language { Id = 75, Name = "Khmer" },
                new Language { Id = 76, Name = "Twi" },
                new Language { Id = 77, Name = "Amharic" },
                new Language { Id = 78, Name = "Tigrinya" },
                new Language { Id = 79, Name = "Maldivian" },
                new Language { Id = 80, Name = "Oromo" },
                new Language { Id = 81, Name = "Fula" },
                new Language { Id = 82, Name = "Chichewa" },
                new Language { Id = 83, Name = "Bambara" },
                new Language { Id = 84, Name = "Tswana" },
                new Language { Id = 85, Name = "Shona" },
                new Language { Id = 86, Name = "Sesotho" },
                new Language { Id = 87, Name = "Wolof" },
                new Language { Id = 88, Name = "Dzongkha" },
                new Language { Id = 89, Name = "Kanuri" },
                new Language { Id = 90, Name = "Ga" },
                new Language { Id = 91, Name = "Acholi" },
                new Language { Id = 92, Name = "Ewe" },
                new Language { Id = 93, Name = "Bislama" },
                new Language { Id = 94, Name = "Tok Pisin" },
                new Language { Id = 95, Name = "Nauruan" },
                new Language { Id = 96, Name = "Chamorro" },
                new Language { Id = 97, Name = "Palauan" },
                new Language { Id = 98, Name = "Tuvaluan" },
                new Language { Id = 99, Name = "Marshallese" },
                new Language { Id = 100, Name = "Tetum" }
            };
        }
    }
}