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
using Mottrist.Service.Features.Drivers.Interfaces;
using Mottrist.Service.Features.Drivers.Mappers;
using Mottrist.Service.Features.Drivers.Services;
using Mottrist.Service.Features.General.Mapper.Profiles;
using Mottrist.Service.Features.Traveller.Interfaces;
using Mottrist.Service.Features.Traveller.Services;
using Mottrist.Service.Features.User;
using Mottrist.Service.Features.User.Inerfaces;
using Mottrist.Utilities.Identity;

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
    //option.Password.RequiredLength = 4;
    //option.Password.RequireDigit = false;
    //option.Password.RequireNonAlphanumeric = false;
    //option.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();



#region AutoMapper
//var mapperConfig = new MapperConfiguration(cfg =>
//{
//    cfg.AddProfile(new MappingProfile());
//});

//IMapper mapper = mapperConfig.CreateMapper();
//builder.Services.AddSingleton(mapper);


builder.Services.AddAutoMapper(typeof(MappingProfile), typeof(TravelerProfile));
builder.Services.AddAutoMapper(typeof(MappingProfile), typeof(DriverProfile));

//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion

#region Custom Services
builder.Services.AddScoped<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender, EmailSender>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITravelerService, TravelerService>();
builder.Services.AddScoped<IDriverService, DriverService>();

#endregion

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json","v1"));
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapControllers());

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
