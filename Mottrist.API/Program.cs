using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mottrist.Domain.Common.IRepository;
using Mottrist.Domain.Common.IUnitOfWork;
using Mottrist.Domain.Identity;
using Mottrist.Repository.DbInitializer;
using Mottrist.Repository.EntityFrameworkCore.Context;
using Mottrist.Repository.Repository;
using Mottrist.Repository.UnitOfWork;
using Mottrist.Service.Implementations;
using Mottrist.Service.Profiles;
using Mottrist.Utilities.Identity;
using Template.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(option =>
{
    option.Password.RequiredLength = 4;
    option.Password.RequireDigit = false;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

#region AutoMapper
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion

#region Custom Services
builder.Services.AddScoped<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender, EmailSender>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserService, UserService>();
#endregion

var app = builder.Build();
SeedDatabase();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
void SeedDatabase()
{
    using (IServiceScope Scope = app.Services.CreateScope())
    {
        IDbInitializer initializer = Scope.ServiceProvider.GetRequiredService<IDbInitializer>();

        initializer.Initialize();
    }
}
