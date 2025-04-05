using Microsoft.AspNetCore.Identity;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Global;
using Mottrist.Domain.Identity;
using Mottrist.Repository.EntityFrameworkCore.Configurations.TravellersSchemaConfiguration;
using Mottrist.Repository.EntityFrameworkCore.Context;
using Mottrist.Service.Features.Drivers.DTOs;
using Mottrist.Service.Features.Drivers.Interfaces;
using Mottrist.Service.Features.Traveller.DTOs;
using Mottrist.Service.Features.Traveller.Interfaces;
using Mottrist.Service.Features.Traveller.Services;

namespace Mottrist.Service.SeedData
{
    public class SeedDb : ISeedDb
    {

        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IDriverService _driverService;
        private readonly ITravelerService _travelerService;

        public SeedDb
        (AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IDriverService driverService, ITravelerService travelerService)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _driverService = driverService;
            _travelerService = travelerService;
        }


  
     

        public void LoadData()
        {

           
        }

      
    }
}
