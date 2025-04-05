using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Domain.Enums;
using Mottrist.Domain.Identity;
using Mottrist.Domain.LookupEntities;
using Mottrist.Utilities.Identity;

namespace Mottrist.Repository.EntityFrameworkCore.Context
{
    public class AppDbContext :  IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public AppDbContext() : base() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
       
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; } = null!;
        public virtual DbSet<Traveler> Travelers { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Car> Cars { get; set; } = null!;
        public virtual DbSet<Brand> Brands { get; set; } = null!;
        public virtual DbSet<Model> Models { get; set; } = null!;
        public virtual DbSet<BodyType> BodyTypes { get; set; } = null!;
        public virtual DbSet<FuelType> FuelTypes { get; set; } = null!;
        public virtual DbSet<CarImage> CarImages { get; set; } = null!;
        public virtual DbSet<Color> Colors { get; set; } = null!;

        public virtual DbSet<DriverCity> DriverCities { get; set; } = null!;
        public virtual DbSet<DriverCountry> DriverCountries { get; set; } = null!;
        public virtual DbSet<DriverLanguage> DriverLanguages { get; set; } = null!;
        public virtual DbSet<Language> Languages { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

            // Seed Countries
            modelBuilder.Entity<Country>().HasData(
      new Country { Id = 1, Name = "USA", Continent = Continent.NorthAmerica },
      new Country { Id = 2, Name = "Canada", Continent = Continent.NorthAmerica },
      new Country { Id = 3, Name = "UK", Continent = Continent.Europe },
      new Country { Id = 4, Name = "Germany", Continent = Continent.Europe },
      new Country { Id = 5, Name = "France", Continent = Continent.Europe },
      new Country { Id = 6, Name = "Australia", Continent = Continent.Australia },
      new Country { Id = 7, Name = "Brazil", Continent = Continent.SouthAmerica },
      new Country { Id = 8, Name = "Argentina", Continent = Continent.SouthAmerica },
      new Country { Id = 9, Name = "China", Continent = Continent.Asia },
      new Country { Id = 10, Name = "Japan", Continent = Continent.Asia },
      new Country { Id = 11, Name = "India", Continent = Continent.Asia },
      new Country { Id = 12, Name = "South Korea", Continent = Continent.Asia },
      new Country { Id = 13, Name = "South Africa", Continent = Continent.Africa },
      new Country { Id = 14, Name = "Nigeria", Continent = Continent.Africa },
      new Country { Id = 15, Name = "Egypt", Continent = Continent.Africa },
      new Country { Id = 16, Name = "Mexico", Continent = Continent.NorthAmerica },
      new Country { Id = 17, Name = "Italy", Continent = Continent.Europe },
      new Country { Id = 18, Name = "Spain", Continent = Continent.Europe },
      new Country { Id = 19, Name = "Russia", Continent = Continent.Europe },
      new Country { Id = 20, Name = "Turkey", Continent = Continent.Asia }
  );

            // Seed Cities (10 cities per country)
            modelBuilder.Entity<City>().HasData(
                // Cities for USA (CountryId = 1)
                new City { Id = 1, Name = "New York", CountryId = 1 },
                new City { Id = 2, Name = "Los Angeles", CountryId = 1 },
                new City { Id = 3, Name = "Chicago", CountryId = 1 },
                new City { Id = 4, Name = "Houston", CountryId = 1 },
                new City { Id = 5, Name = "Phoenix", CountryId = 1 },
                new City { Id = 6, Name = "Philadelphia", CountryId = 1 },
                new City { Id = 7, Name = "San Antonio", CountryId = 1 },
                new City { Id = 8, Name = "San Diego", CountryId = 1 },
                new City { Id = 9, Name = "Dallas", CountryId = 1 },
                new City { Id = 10, Name = "San Jose", CountryId = 1 },

                // Cities for Canada (CountryId = 2)
                new City { Id = 11, Name = "Toronto", CountryId = 2 },
                new City { Id = 12, Name = "Montreal", CountryId = 2 },
                new City { Id = 13, Name = "Vancouver", CountryId = 2 },
                new City { Id = 14, Name = "Calgary", CountryId = 2 },
                new City { Id = 15, Name = "Ottawa", CountryId = 2 },
                new City { Id = 16, Name = "Edmonton", CountryId = 2 },
                new City { Id = 17, Name = "Winnipeg", CountryId = 2 },
                new City { Id = 18, Name = "Quebec City", CountryId = 2 },
                new City { Id = 19, Name = "Hamilton", CountryId = 2 },
                new City { Id = 20, Name = "Kitchener", CountryId = 2 },

                // Cities for UK (CountryId = 3)
                new City { Id = 21, Name = "London", CountryId = 3 },
                new City { Id = 22, Name = "Manchester", CountryId = 3 },
                new City { Id = 23, Name = "Birmingham", CountryId = 3 },
                new City { Id = 24, Name = "Liverpool", CountryId = 3 },
                new City { Id = 25, Name = "Leeds", CountryId = 3 },
                new City { Id = 26, Name = "Sheffield", CountryId = 3 },
                new City { Id = 27, Name = "Edinburgh", CountryId = 3 },
                new City { Id = 28, Name = "Glasgow", CountryId = 3 },
                new City { Id = 29, Name = "Bristol", CountryId = 3 },
                new City { Id = 30, Name = "Nottingham", CountryId = 3 },

                // Cities for Germany (CountryId = 4)
                new City { Id = 31, Name = "Berlin", CountryId = 4 },
                new City { Id = 32, Name = "Munich", CountryId = 4 },
                new City { Id = 33, Name = "Frankfurt", CountryId = 4 },
                new City { Id = 34, Name = "Hamburg", CountryId = 4 },
                new City { Id = 35, Name = "Cologne", CountryId = 4 },
                new City { Id = 36, Name = "Stuttgart", CountryId = 4 },
                new City { Id = 37, Name = "Düsseldorf", CountryId = 4 },
                new City { Id = 38, Name = "Dortmund", CountryId = 4 },
                new City { Id = 39, Name = "Essen", CountryId = 4 },
                new City { Id = 40, Name = "Bremen", CountryId = 4 },

                // Cities for France (CountryId = 5)
                new City { Id = 41, Name = "Paris", CountryId = 5 },
                new City { Id = 42, Name = "Marseille", CountryId = 5 },
                new City { Id = 43, Name = "Lyon", CountryId = 5 },
                new City { Id = 44, Name = "Toulouse", CountryId = 5 },
                new City { Id = 45, Name = "Nice", CountryId = 5 },
                new City { Id = 46, Name = "Nantes", CountryId = 5 },
                new City { Id = 47, Name = "Strasbourg", CountryId = 5 },
                new City { Id = 48, Name = "Montpellier", CountryId = 5 },
                new City { Id = 49, Name = "Bordeaux", CountryId = 5 },
                new City { Id = 50, Name = "Lille", CountryId = 5 },

                // Cities for Australia (CountryId = 6)
                new City { Id = 51, Name = "Sydney", CountryId = 6 },
                new City { Id = 52, Name = "Melbourne", CountryId = 6 },
                new City { Id = 53, Name = "Brisbane", CountryId = 6 },
                new City { Id = 54, Name = "Perth", CountryId = 6 },
                new City { Id = 55, Name = "Adelaide", CountryId = 6 },
                new City { Id = 56, Name = "Gold Coast", CountryId = 6 },
                new City { Id = 57, Name = "Hobart", CountryId = 6 },
                new City { Id = 58, Name = "Canberra", CountryId = 6 },
                new City { Id = 59, Name = "Newcastle", CountryId = 6 },
                new City { Id = 60, Name = "Wollongong", CountryId = 6 },

                // Cities for Brazil (CountryId = 7)
                new City { Id = 61, Name = "São Paulo", CountryId = 7 },
                new City { Id = 62, Name = "Rio de Janeiro", CountryId = 7 },
                new City { Id = 63, Name = "Brasília", CountryId = 7 },
                new City { Id = 64, Name = "Salvador", CountryId = 7 },
                new City { Id = 65, Name = "Fortaleza", CountryId = 7 },
                new City { Id = 66, Name = "Belo Horizonte", CountryId = 7 },
                new City { Id = 67, Name = "Manaus", CountryId = 7 },
                new City { Id = 68, Name = "Curitiba", CountryId = 7 },
                new City { Id = 69, Name = "Recife", CountryId = 7 },
                new City { Id = 70, Name = "Porto Alegre", CountryId = 7 },

                // Cities for Argentina (CountryId = 8)
                new City { Id = 71, Name = "Buenos Aires", CountryId = 8 },
                new City { Id = 72, Name = "Córdoba", CountryId = 8 },
                new City { Id = 73, Name = "Rosario", CountryId = 8 },
                new City { Id = 74, Name = "Mendoza", CountryId = 8 },
                new City { Id = 75, Name = "La Plata", CountryId = 8 },
                new City { Id = 76, Name = "San Miguel de Tucumán", CountryId = 8 },
                new City { Id = 77, Name = "Mar del Plata", CountryId = 8 },
                new City { Id = 78, Name = "Salta", CountryId = 8 },
                new City { Id = 79, Name = "Santa Fe", CountryId = 8 },
                new City { Id = 80, Name = "Santiago del Estero", CountryId = 8 },

                // Cities for China (CountryId = 9)
                new City { Id = 81, Name = "Beijing", CountryId = 9 },
                new City { Id = 82, Name = "Shanghai", CountryId = 9 },
                new City { Id = 83, Name = "Guangzhou", CountryId = 9 },
                new City { Id = 84, Name = "Shenzhen", CountryId = 9 },
                new City { Id = 85, Name = "Chengdu", CountryId = 9 },
                new City { Id = 86, Name = "Hangzhou", CountryId = 9 },
                new City { Id = 87, Name = "Xi'an", CountryId = 9 },
                new City { Id = 88, Name = "Wuhan", CountryId = 9 },
                new City { Id = 89, Name = "Chongqing", CountryId = 9 },
                new City { Id = 90, Name = "Tianjin", CountryId = 9 }
            );

            modelBuilder.Entity<ApplicationRole>().HasData(
            new ApplicationRole { Id = 1, Name = AppUserRoles.RoleAdmin, NormalizedName = "ADMIN" },
            new ApplicationRole { Id = 2, Name = AppUserRoles.RoleDriver, NormalizedName = "DRIVER" },
            new ApplicationRole { Id = 3, Name = AppUserRoles.RoleTraveler, NormalizedName = "TRAVELER" },
            new ApplicationRole { Id = 4, Name = AppUserRoles.RoleEmployee, NormalizedName = "EMPLOYEE" }
       );

        }
     
    }
}
