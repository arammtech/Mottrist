using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mottrist.Domain.Enums;
using Mottrist.Domain.LookupEntities;

namespace Mottrist.Repository.EntityFrameworkCore.Configurations.GeographySchemaConfiguration
{
    internal class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            // Table mapping
            builder.ToTable("Countries", schema: "Geography");
            builder.HasData(GetCountries());
        }

        private List<Country> GetCountries()
        {
            return new List<Country>
            {
                new Country { Id = 1, Name = "United States" },
                new Country { Id = 2, Name = "Canada" },
                new Country { Id = 3, Name = "Mexico" },
                new Country { Id = 4, Name = "Brazil" },
                new Country { Id = 5, Name = "Argentina" },
                new Country { Id = 6, Name = "United Kingdom" },
                new Country { Id = 7, Name = "France" },
                new Country { Id = 8, Name = "Germany" },
                new Country { Id = 9, Name = "Italy" },
                new Country { Id = 10, Name = "Spain" },
                new Country { Id = 11, Name = "China" },
                new Country { Id = 12, Name = "Japan" },
                new Country { Id = 13, Name = "India" },
                new Country { Id = 14, Name = "South Korea" },
                new Country { Id = 15, Name = "Indonesia" },
                new Country { Id = 16, Name = "Russia" },
                new Country { Id = 17, Name = "South Africa" },
                new Country { Id = 18, Name = "Egypt" },
                new Country { Id = 19, Name = "Nigeria" },
                new Country { Id = 20, Name = "Kenya" },
                new Country { Id = 21, Name = "Australia" },
                new Country { Id = 22, Name = "New Zealand" },
                new Country { Id = 23, Name = "Saudi Arabia" },
                new Country { Id = 24, Name = "Turkey" },
                new Country { Id = 25, Name = "Pakistan" },
                new Country { Id = 26, Name = "Bangladesh" },
                new Country { Id = 27, Name = "Vietnam" },
                new Country { Id = 28, Name = "Thailand" },
                new Country { Id = 29, Name = "Iran" },
                new Country { Id = 30, Name = "Israel" },
                new Country { Id = 31, Name = "Malaysia" },
                new Country { Id = 32, Name = "Philippines" },
                new Country { Id = 33, Name = "Portugal" },
                new Country { Id = 34, Name = "Greece" },
                new Country { Id = 35, Name = "Netherlands" },
                new Country { Id = 36, Name = "Sweden" },
                new Country { Id = 37, Name = "Norway" },
                new Country { Id = 38, Name = "Denmark" },
                new Country { Id = 39, Name = "Switzerland" },
                new Country { Id = 40, Name = "Poland" },
                new Country { Id = 41, Name = "Belgium" },
                new Country { Id = 42, Name = "Romania" },
                new Country { Id = 43, Name = "Czech Republic" },
                new Country { Id = 44, Name = "Hungary" },
                new Country { Id = 45, Name = "Ukraine" },
                new Country { Id = 46, Name = "Colombia" },
                new Country { Id = 47, Name = "Chile" },
                new Country { Id = 48, Name = "Peru" },
                new Country { Id = 49, Name = "Venezuela" },
                new Country { Id = 50, Name = "Ecuador" },
                new Country { Id = 51, Name = "United Arab Emirates" },
                new Country { Id = 52, Name = "Singapore" },
                new Country { Id = 53, Name = "Qatar" },
                new Country { Id = 54, Name = "South Sudan" },
                new Country { Id = 55, Name = "Myanmar" },
                new Country { Id = 56, Name = "Kazakhstan" },
                new Country { Id = 57, Name = "Algeria" },
                new Country { Id = 58, Name = "Morocco" },
                new Country { Id = 59, Name = "Tunisia" },
                new Country { Id = 60, Name = "Lebanon" },
                new Country { Id = 61, Name = "Jordan" },
                new Country { Id = 62, Name = "Sri Lanka" },
                new Country { Id = 63, Name = "Nepal" },
                new Country { Id = 64, Name = "Mongolia" },
                new Country { Id = 65, Name = "Uzbekistan" },
                new Country { Id = 66, Name = "Turkmenistan" },
                new Country { Id = 67, Name = "Bolivia" },
                new Country { Id = 68, Name = "Paraguay" },
                new Country { Id = 69, Name = "Uruguay" },
                new Country { Id = 70, Name = "Costa Rica" },
                new Country { Id = 71, Name = "Panama" },
                new Country { Id = 72, Name = "Honduras" },
                new Country { Id = 73, Name = "El Salvador" },
                new Country { Id = 74, Name = "Guatemala" },
                new Country { Id = 75, Name = "Dominican Republic" },
                new Country { Id = 76, Name = "Kuwait" },
                new Country { Id = 77, Name = "Bahrain" },
                new Country { Id = 78, Name = "Oman" },
                new Country { Id = 79, Name = "Iraq" },
                new Country { Id = 80, Name = "Syria" },
                new Country { Id = 81, Name = "Yemen" },
                new Country { Id = 82, Name = "Sudan" },
                new Country { Id = 83, Name = "Ethiopia" },
                new Country { Id = 84, Name = "Tanzania" },
                new Country { Id = 85, Name = "Zambia" },
                new Country { Id = 86, Name = "Zimbabwe" },
                new Country { Id = 87, Name = "Botswana" },
                new Country { Id = 88, Name = "Namibia" },
                new Country { Id = 89, Name = "Madagascar" },
                new Country { Id = 90, Name = "Papua New Guinea" },
                new Country { Id = 91, Name = "Fiji" },
                new Country { Id = 92, Name = "Solomon Islands" },
                new Country { Id = 93, Name = "Brunei" },
                new Country { Id = 94, Name = "Malawi" },
                new Country { Id = 95, Name = "Burundi" },
                new Country { Id = 96, Name = "Rwanda" },
                new Country { Id = 97, Name = "Ivory Coast" },
                new Country { Id = 98, Name = "Senegal" },
                new Country { Id = 99, Name = "Ghana" },
                new Country { Id = 100, Name = "Cameroon" }
            };
        }
    }
}
