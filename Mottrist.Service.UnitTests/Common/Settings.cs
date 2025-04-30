using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Mottrist.Domain.Identity;
using Mottrist.Repository.EntityFrameworkCore.Context;
using Mottrist.Service.Features.Drivers.Profiles;
using Mottrist.Service.Features.General.Mapper.Profiles;
using Mottrist.Service.Features.Traveller.Profiles;
using Mottrist.Service.Features.Traveller.Validators;

namespace Mottrist.Service.UnitTests.Common
{
    public class Settings
    {
        public const string ConnectionString = "Server=.;Database=Mottrist;Trusted_Connection=True;TrustServerCertificate=True";

        private static readonly DbContextOptions<AppDbContext> _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(ConnectionString)
            .Options;

        public static readonly IConfigurationProvider MapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        private static readonly Lazy<IServiceProvider> _serviceProviderLazy = new Lazy<IServiceProvider>(() => InitializeServiceProvider());

        public  IServiceProvider ServiceProvider => _serviceProviderLazy.Value;

        private static IServiceProvider InitializeServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(ConnectionString));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();

            services.AddLogging();
            services.AddAutoMapper(typeof(MappingProfile), typeof(TravelerProfile), typeof(DriverProfile));
            services.AddFluentValidationAutoValidation()
                            .AddValidatorsFromAssembly(typeof(AddTravelerDtoValidator).Assembly);

            services.AddSingleton(MapperConfig.CreateMapper());

            return services.BuildServiceProvider();
        }

        public AppDbContext Context => ServiceProvider.GetRequiredService<AppDbContext>();

        public IMapper Mapper => ServiceProvider.GetRequiredService<IMapper>();

        public Settings()
        {
            // Ensure the database is created
            Context.Database.EnsureCreated();
        }
    }
}
