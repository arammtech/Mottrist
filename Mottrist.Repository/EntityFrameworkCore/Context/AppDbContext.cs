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
                new Country { Id = 1, Name = "USA" ,Continent = Continent.NorthAmerica},
                new Country { Id = 2, Name = "Canada", Continent = Continent.NorthAmerica },
                new Country { Id = 3, Name = "UK", Continent = Continent.Europe },
                new Country { Id = 4, Name = "Germany", Continent = Continent.Europe },
                new Country { Id = 5, Name = "France", Continent = Continent.Europe }
            );

            // Seed Cities (5 cities per country)
            modelBuilder.Entity<City>().HasData(
                // Cities for USA (CountryId = 1)
                new City { Id = 1, Name = "New York", CountryId = 1 },
                new City { Id = 2, Name = "Los Angeles", CountryId = 1 },
                new City { Id = 3, Name = "Chicago", CountryId = 1 },
                new City { Id = 4, Name = "Houston", CountryId = 1 },
                new City { Id = 5, Name = "Phoenix", CountryId = 1 },

                // Cities for Canada (CountryId = 2)
                new City { Id = 6, Name = "Toronto", CountryId = 2 },
                new City { Id = 7, Name = "Montreal", CountryId = 2 },
                new City { Id = 8, Name = "Vancouver", CountryId = 2 },
                new City { Id = 9, Name = "Calgary", CountryId = 2 },
                new City { Id = 10, Name = "Ottawa", CountryId = 2 },

                // Cities for UK (CountryId = 3)
                new City { Id = 11, Name = "London", CountryId = 3 },
                new City { Id = 12, Name = "Manchester", CountryId = 3 },
                new City { Id = 13, Name = "Birmingham", CountryId = 3 },
                new City { Id = 14, Name = "Liverpool", CountryId = 3 },
                new City { Id = 15, Name = "Leeds", CountryId = 3 },

                // Cities for Germany (CountryId = 4)
                new City { Id = 16, Name = "Berlin", CountryId = 4 },
                new City { Id = 17, Name = "Munich", CountryId = 4 },
                new City { Id = 18, Name = "Frankfurt", CountryId = 4 },
                new City { Id = 19, Name = "Hamburg", CountryId = 4 },
                new City { Id = 20, Name = "Cologne", CountryId = 4 },

                // Cities for France (CountryId = 5)
                new City { Id = 21, Name = "Paris", CountryId = 5 },
                new City { Id = 22, Name = "Marseille", CountryId = 5 },
                new City { Id = 23, Name = "Lyon", CountryId = 5 },
                new City { Id = 24, Name = "Toulouse", CountryId = 5 },
                new City { Id = 25, Name = "Nice", CountryId = 5 }
            );

            modelBuilder.Entity<ApplicationRole>().HasData(
                 new ApplicationRole { Id = 1, Name = AppUserRoles.RoleAdmin, NormalizedName = "ADMIN" },
                 new ApplicationRole { Id = 2, Name =AppUserRoles.RoleDriver, NormalizedName = "DRIVER" },
                 new ApplicationRole { Id = 3, Name = AppUserRoles.RoleTraveler, NormalizedName = "TRAVELER" },
                 new ApplicationRole { Id = 4, Name = AppUserRoles.RoleEmployee, NormalizedName = "EMPLOYEE" }
            );

        }
     
    }
}
