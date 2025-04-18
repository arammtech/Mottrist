using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Mottrist.Domain.Common.IRepository;
using Mottrist.Domain.Common.IUnitOfWork;
using Mottrist.Domain.Identity;
using Mottrist.Repository.DbInitializer;
using Mottrist.Repository.EntityFrameworkCore.Context;
using Mottrist.Repository.Repository;
using Mottrist.Repository.UnitOfWork;
using Mottrist.Service.Features.Cars.Interfaces;
using Mottrist.Service.Features.Cars.Services;
using Mottrist.Service.Features.Drivers.Interfaces;
using Mottrist.Service.Features.Drivers.Mappers;
using Mottrist.Service.Features.Drivers.Services;
using Mottrist.Service.Features.General.Mapper.Profiles;
using Mottrist.Service.Features.Traveller.Interfaces;
using Mottrist.Service.Features.Traveller.Mappers;
using Mottrist.Service.Features.Traveller.Services;
using Mottrist.Service.Features.Traveller.Validators;
using Mottrist.Utilities.Identity;
using System.Reflection;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Mottrist.Service.Features.Cities.Interfaces;
using Mottrist.Service.Features.Cities.Services;
using Mottrist.Service.Features.Countries.Interfaces;
using Mottrist.Service.Features.Countries.Services;
using Mottrist.Service.Features.Languages.Interfaces;
using Mottrist.Service.Features.Languages.Services;
using Mottrist.Service.Features.Cars.Interfaces.CarFields;
using Mottrist.Service.Features.Cars.Services.CarFields;
using Mottrist.Service.SeedData;

var builder = WebApplication.CreateBuilder(args);

#region  APIsControllers Configuration
builder.Services.AddControllers();
#endregion

#region Swagger/OpenAPI Configuration
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Mottrist",
        Version = "v1"
    });

    // Locate the XML file generated by the compiler
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    // Tell Swagger to use XML comments
    options.IncludeXmlComments(xmlPath);

    // To Enable authorization using Swagger (JWT)
    // JWT Bearer security definition for Swagger UI
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by a space and then your token"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// Optional: if you already have an extension method for OpenAPI, you can remove or modify it
// builder.Services.AddOpenApi();
#endregion

#region ApiBehaviorOptions  Configuration
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = false; // To allow automatic validation!
});

#endregion

#region EF Configuration
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
#endregion

#region Identity Configuration
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
#endregion

#region Cors Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("MottristPolicy",
        CorsPolicyBuilder =>
        {
            CorsPolicyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
});
#endregion

#region Authentication (JWT) Configuration
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//{
//    options.SaveToken = true;
//    options.RequireHttpsMetadata = false; // change this to true later
//    options.TokenValidationParameters = new()
//    {
//        ValidateIssuer = true,
//        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
//        ValidateAudience = true,
//        ValidAudience = builder.Configuration["JWT:ValidAudience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
//    };
//});
#endregion

#region Packages 
builder.Services.AddAutoMapper(typeof(MappingProfile), typeof(TravelerProfile), typeof(DriverProfile));
builder.Services.AddFluentValidationAutoValidation()
                .AddValidatorsFromAssembly(typeof(AddTravelerDtoValidator).Assembly);
#endregion

#region Custom Services
builder.Services.AddScoped<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender, EmailSender>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<ITravelerService, TravelerService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ILanguageService, LanguageService>();
builder.Services.AddScoped<ICarModelService, CarModelService>();
builder.Services.AddScoped<ICarColorService, CarColorService>();
builder.Services.AddScoped<ICarBodyTypeService, CarBodyTypeService>();
builder.Services.AddScoped<ICarFuelTypeService, CarFuelTypeService>();
builder.Services.AddScoped<ICarBrandService, CarBrandService>();
builder.Services.AddScoped<ISeedDb, SeedDb>();
#endregion

var app = builder.Build();

SeedDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Map Swagger/OpenAPI endpoints
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Mottrist V1");
    });
}
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCors("MottristPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.MapControllers();

app.Run();

void SeedDatabase()
{
    using (IServiceScope scope = app.Services.CreateScope())
    {
        IDbInitializer initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        initializer.Initialize();
    }
}
