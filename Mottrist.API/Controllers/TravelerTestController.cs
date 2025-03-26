using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mottrist.Domain.Common.IUnitOfWork;
using Mottrist.Domain.Enums;
using Mottrist.Domain.Identity;
using Mottrist.Domain.LookupEntities;
using Mottrist.Service.Features.Traveller.DTOs;
using Mottrist.Service.Features.Traveller.Interfaces;
using Mottrist.Service.Features.Traveller.Services;
using Mottrist.Service.Features.User.DTOs;
using Mottrist.Service.Features.User.Inerfaces;
using Mottrist.Utilities.Identity;

namespace Mottrist.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelerTestController : ControllerBase
    {
        private readonly ITravelerService _travelerService;

        public TravelerTestController( ITravelerService travelerService)
        {
            _travelerService = travelerService;
        }

        [HttpGet]
        public async Task<IActionResult> AddUser()
        {
            try
            {
                // Create a sample traveler
                AddUpdateTravelerDto travelerDto = new AddUpdateTravelerDto
                {
                    WhatsAppNumber = "+201234567890",
                    NationailtyId = 1,
                        FirstName = "Asigndfegd",
                        LastName = "Mdfdogfiud",
                        Email = "ahed.maoffggsfdfd0nbv13@example.com",
                        UserName = "ahdrdfgfffbdfgied",
                        PhoneNumber = "01123456789",
                        PasswordHash = "Pass@1234",
                        ProfileImageUrl = "https://example.com/images/ahmed.jpg",
                };

                var result = await _travelerService.AddAsync(travelerDto);

                if (result.IsSuccess)
                {
                    return Ok("Sucess");
                }
                else
                {
                    return StatusCode(500, $"Error");
                }
            }
               catch (HttpRequestException ex)
                {
                // Return a 500 error with the exception message if an error occurs
                return StatusCode(500, $"Error  : {ex.Message}");
            }
        }

    }
}
