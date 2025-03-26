using AutoMapper;
using Feature.Car.DTOs;
using Moq;
using Mottrist.Domain.Common.IUnitOfWork;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Domain.Enums;
using Mottrist.Domain.Global;
using Mottrist.Domain.LookupEntities;
using Mottrist.Service.Features.Cars.Interfaces;
using Mottrist.Service.Features.Cars.Services;
using Mottrist.Service.Features.Traveller.DTOs;
using Mottrist.Service.Features.User.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mottrist.Service.Features.Traveller.Interfaces;
using Mottrist.Service.Features.User.Inerfaces;
using Mottrist.Utilities.Identity;

namespace Mottrist.Service.UnitTests.Features.Traveler
{
    public class TravelerServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ICarService _carService;

        public TravelerServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _carService = new CarService(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldReturnSuccess_WhenCarIsCreated()
        {
           

       
        }


    }
}
