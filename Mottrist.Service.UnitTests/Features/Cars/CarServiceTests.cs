using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Xunit;
using Mottrist.Domain.Common.IUnitOfWork;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Domain.Global;
using Mottrist.Service.Features.Cars.Services;
using Mottrist.Service.Features.Cars.Interfaces;
using Feature.Car.DTOs;
using Microsoft.AspNetCore.Identity;
using Mottrist.Domain.Identity;
using Mottrist.Repository.EntityFrameworkCore.Context;
using Mottrist.Repository.UnitOfWork;
using Mottrist.Service.UnitTests.Common;
using Microsoft.Extensions.DependencyInjection;
using Mottrist.Service.Features.Drivers.Services;

namespace Mottrist.Service.UnitTests.Features.Cars
{
    public class CarServiceTests : IClassFixture<Settings>
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CarServiceTests(Settings settings)
        {
            _context = settings.Context;
            _userManager = settings.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            _roleManager = settings.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            _mapper = settings.Mapper;
            _unitOfWork = new UnitOfWork(_context);
        }


        [Fact]
        public async Task AddCarAsync_ShouldAddCar()
        {
            // Arrange
            var carService = new CarService(_unitOfWork, _mapper);
            var driverService = new DriverService(_unitOfWork, _mapper, carService, _userManager);

            var result = await driverService.DeleteImageCarAsync(1024, "/images/cars/af32bfa1-faed-4f60-8923-0a5edc0ba186_11.png");
            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}
