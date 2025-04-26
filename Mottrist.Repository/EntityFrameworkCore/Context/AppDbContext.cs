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
        public virtual DbSet<DriverInteraction> DriverInteractions { get; set; }
        public virtual DbSet<Language> Languages { get; set; } = null!;
        public virtual DbSet<Destination> Destinations { get; set; } = null!;
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
                new City { Id = 90, Name = "Tianjin", CountryId = 9 },

                // Cities for Japan (CountryId = 10)
                new City { Id = 91, Name = "Tokyo", CountryId = 10 },
                new City { Id = 92, Name = "Osaka", CountryId = 10 },
                new City { Id = 93, Name = "Kyoto", CountryId = 10 },
                new City { Id = 94, Name = "Yokohama", CountryId = 10 },
                new City { Id = 95, Name = "Fukuoka", CountryId = 10 },
                new City { Id = 96, Name = "Sapporo", CountryId = 10 },
                new City { Id = 97, Name = "Nagoya", CountryId = 10 },
                new City { Id = 98, Name = "Kobe", CountryId = 10 },
                new City { Id = 99, Name = "Hiroshima", CountryId = 10 },
                new City { Id = 100, Name = "Sendai", CountryId = 10 },

                // Cities for India (CountryId = 11)
                new City { Id = 101, Name = "Mumbai", CountryId = 11 },
                new City { Id = 102, Name = "Delhi", CountryId = 11 },
                new City { Id = 103, Name = "Bangalore", CountryId = 11 },
                new City { Id = 104, Name = "Hyderabad", CountryId = 11 },
                new City { Id = 105, Name = "Chennai", CountryId = 11 },
                new City { Id = 106, Name = "Kolkata", CountryId = 11 },
                new City { Id = 107, Name = "Ahmedabad", CountryId = 11 },
                new City { Id = 108, Name = "Pune", CountryId = 11 },
                new City { Id = 109, Name = "Jaipur", CountryId = 11 },
                new City { Id = 110, Name = "Lucknow", CountryId = 11 },

                // Cities for South Korea (CountryId = 12)
                new City { Id = 111, Name = "Seoul", CountryId = 12 },
                new City { Id = 112, Name = "Busan", CountryId = 12 },
                new City { Id = 113, Name = "Incheon", CountryId = 12 },
                new City { Id = 114, Name = "Daegu", CountryId = 12 },
                new City { Id = 115, Name = "Daejeon", CountryId = 12 },
                new City { Id = 116, Name = "Gwangju", CountryId = 12 },
                new City { Id = 117, Name = "Suwon", CountryId = 12 },
                new City { Id = 118, Name = "Ulsan", CountryId = 12 },
                new City { Id = 119, Name = "Jeonju", CountryId = 12 },
                new City { Id = 120, Name = "Goyang", CountryId = 12 },

                // Cities for South Africa (CountryId = 13)
                new City { Id = 121, Name = "Johannesburg", CountryId = 13 },
                new City { Id = 122, Name = "Cape Town", CountryId = 13 },
                new City { Id = 123, Name = "Durban", CountryId = 13 },
                new City { Id = 124, Name = "Pretoria", CountryId = 13 },
                new City { Id = 125, Name = "Port Elizabeth", CountryId = 13 },
                new City { Id = 126, Name = "Bloemfontein", CountryId = 13 },
                new City { Id = 127, Name = "East London", CountryId = 13 },
                new City { Id = 128, Name = "Polokwane", CountryId = 13 },
                new City { Id = 129, Name = "Nelspruit", CountryId = 13 },
                new City { Id = 130, Name = "Kimberley", CountryId = 13 },

                // Cities for Nigeria (CountryId = 14)
                new City { Id = 131, Name = "Lagos", CountryId = 14 },
                new City { Id = 132, Name = "Abuja", CountryId = 14 },
                new City { Id = 133, Name = "Kano", CountryId = 14 },
                new City { Id = 134, Name = "Ibadan", CountryId = 14 },
                new City { Id = 135, Name = "Port Harcourt", CountryId = 14 },
                new City { Id = 136, Name = "Benin City", CountryId = 14 },
                new City { Id = 137, Name = "Kaduna", CountryId = 14 },
                new City { Id = 138, Name = "Maiduguri", CountryId = 14 },
                new City { Id = 139, Name = "Enugu", CountryId = 14 },
                new City { Id = 140, Name = "Jos", CountryId = 14 },

                // Cities for Egypt (CountryId = 15)
                new City { Id = 141, Name = "Cairo", CountryId = 15 },
                new City { Id = 142, Name = "Alexandria", CountryId = 15 },
                new City { Id = 143, Name = "Giza", CountryId = 15 },
                new City { Id = 144, Name = "Shubra El Kheima", CountryId = 15 },
                new City { Id = 145, Name = "Port Said", CountryId = 15 },
                new City { Id = 146, Name = "Suez", CountryId = 15 },
                new City { Id = 147, Name = "Luxor", CountryId = 15 },
                new City { Id = 148, Name = "Aswan", CountryId = 15 },
                new City { Id = 149, Name = "Ismailia", CountryId = 15 },
                new City { Id = 150, Name = "Mansoura", CountryId = 15 },

                // Cities for Mexico (CountryId = 16)
                new City { Id = 151, Name = "Mexico City", CountryId = 16 },
                new City { Id = 152, Name = "Guadalajara", CountryId = 16 },
                new City { Id = 153, Name = "Monterrey", CountryId = 16 },
                new City { Id = 154, Name = "Puebla", CountryId = 16 },
                new City { Id = 155, Name = "Tijuana", CountryId = 16 },
                new City { Id = 156, Name = "León", CountryId = 16 },
                new City { Id = 157, Name = "Cancún", CountryId = 16 },
                new City { Id = 158, Name = "Mérida", CountryId = 16 },
                new City { Id = 159, Name = "Toluca", CountryId = 16 },
                new City { Id = 160, Name = "Chihuahua", CountryId = 16 },

                // Cities for Italy (CountryId = 17)
                new City { Id = 161, Name = "Rome", CountryId = 17 },
                new City { Id = 162, Name = "Milan", CountryId = 17 },
                new City { Id = 163, Name = "Naples", CountryId = 17 },
                new City { Id = 164, Name = "Turin", CountryId = 17 },
                new City { Id = 165, Name = "Palermo", CountryId = 17 },
                new City { Id = 166, Name = "Genoa", CountryId = 17 },
                new City { Id = 167, Name = "Bologna", CountryId = 17 },
                new City { Id = 168, Name = "Florence", CountryId = 17 },
                new City { Id = 169, Name = "Venice", CountryId = 17 },
                new City { Id = 170, Name = "Verona", CountryId = 17 },

                // Cities for Spain (CountryId = 18)
                new City { Id = 171, Name = "Madrid", CountryId = 18 },
                new City { Id = 172, Name = "Barcelona", CountryId = 18 },
                new City { Id = 173, Name = "Valencia", CountryId = 18 },
                new City { Id = 174, Name = "Seville", CountryId = 18 },
                new City { Id = 175, Name = "Zaragoza", CountryId = 18 },
                new City { Id = 176, Name = "Málaga", CountryId = 18 },
                new City { Id = 177, Name = "Murcia", CountryId = 18 },
                new City { Id = 178, Name = "Bilbao", CountryId = 18 },
                new City { Id = 179, Name = "Alicante", CountryId = 18 },
                new City { Id = 180, Name = "Granada", CountryId = 18 },

                // Cities for Russia (CountryId = 19)
                new City { Id = 181, Name = "Moscow", CountryId = 19 },
                new City { Id = 182, Name = "Saint Petersburg", CountryId = 19 },
                new City { Id = 183, Name = "Novosibirsk", CountryId = 19 },
                new City { Id = 184, Name = "Yekaterinburg", CountryId = 19 },
                new City { Id = 185, Name = "Kazan", CountryId = 19 },
                new City { Id = 186, Name = "Nizhny Novgorod", CountryId = 19 },
                new City { Id = 187, Name = "Chelyabinsk", CountryId = 19 },
                new City { Id = 188, Name = "Samara", CountryId = 19 },
                new City { Id = 189, Name = "Omsk", CountryId = 19 },
                new City { Id = 190, Name = "Rostov-on-Don", CountryId = 19 },

                // Cities for Turkey (CountryId = 20)
                new City { Id = 191, Name = "Istanbul", CountryId = 20 },
                new City { Id = 192, Name = "Ankara", CountryId = 20 },
                new City { Id = 193, Name = "Izmir", CountryId = 20 },
                new City { Id = 194, Name = "Bursa", CountryId = 20 },
                new City { Id = 195, Name = "Adana", CountryId = 20 },
                new City { Id = 196, Name = "Gaziantep", CountryId = 20 },
                new City { Id = 197, Name = "Konya", CountryId = 20 },
                new City { Id = 198, Name = "Antalya", CountryId = 20 },
                new City { Id = 199, Name = "Kayseri", CountryId = 20 },
                new City { Id = 200, Name = "Mersin", CountryId = 20 }

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
