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
        public virtual DbSet<Message> Messages { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);         

            modelBuilder.Entity<ApplicationRole>().HasData(
            new ApplicationRole { Id = 1, Name = AppUserRoles.RoleAdmin, NormalizedName = "ADMIN" },
            new ApplicationRole { Id = 2, Name = AppUserRoles.RoleDriver, NormalizedName = "DRIVER" },
            new ApplicationRole { Id = 3, Name = AppUserRoles.RoleTraveler, NormalizedName = "TRAVELER" },
            new ApplicationRole { Id = 4, Name = AppUserRoles.RoleEmployee, NormalizedName = "EMPLOYEE" }
       );

        }
     
    }
}
