using Microsoft.AspNetCore.Identity;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Global;
using Mottrist.Domain.Identity;
using Mottrist.Repository.EntityFrameworkCore.Configurations.TravellersSchemaConfiguration;
using Mottrist.Repository.EntityFrameworkCore.Context;
using Mottrist.Service.Features.Traveller.DTOs;
using Mottrist.Service.Features.Traveller.Interfaces;
using Mottrist.Service.SeedData;

namespace Mottrist.Repository.SeedData
{
    public  class SeedDatabaseTables : ISeedDatabaseTables
    {
      
        private readonly ITravelerService _travelerService;

        public SeedDatabaseTables(ITravelerService travelerService)
        {
            _travelerService = travelerService;
        }

        public async Task LoadTravelers()
        {
            // Create a list of 11 sample travelers.
            var travelers = new List<AddTravelerDto>
    {
        new AddTravelerDto
        {
            Id = 0,
            WhatsAppNumber = "+12345678901",
            NationalityId = 1,
            FirstName = "John",
            LastName = "Smith",
            Email = "john.smith1@example.com",
            PhoneNumber = "+12345678901",
            Password = "Pass@1234",
            ProfileImageUrl = "https://example.com/images/1.jpg"
        },
        new AddTravelerDto
        {
            Id = 0,
            WhatsAppNumber = "+12345678902",
            NationalityId = 2,
            FirstName = "Jane",
            LastName = "Doe",
            Email = "jane.doe@example.com",
            PhoneNumber = "+12345678902",
            Password = "Pass@1234",
            ProfileImageUrl = "https://example.com/images/2.jpg"
        },
        new AddTravelerDto
        {
            Id = 0,
            WhatsAppNumber = "+12345678903",
            NationalityId = 3,
            FirstName = "Alice",
            LastName = "Brown",
            Email = "alice.brown@example.com",
            PhoneNumber = "+12345678903",
            Password = "Pass@1234",
            ProfileImageUrl = "https://example.com/images/3.jpg"
        },
        new AddTravelerDto
        {
            Id = 0,
            WhatsAppNumber = "+12345678904",
            NationalityId = 4,
            FirstName = "Bob",
            LastName = "Johnson",
            Email = "bob.johnson@example.com",
            PhoneNumber = "+12345678904",
            Password = "Pass@1234",
            ProfileImageUrl = "https://example.com/images/4.jpg"
        },
        new AddTravelerDto
        {
            Id = 0,
            WhatsAppNumber = "+12345678905",
            NationalityId = 5,
            FirstName = "Charlie",
            LastName = "Williams",
            Email = "charlie.williams@example.com",
            PhoneNumber = "+12345678905",
            Password = "Pass@1234",
            ProfileImageUrl = "https://example.com/images/5.jpg"
        },
        new AddTravelerDto
        {
            Id = 0,
            WhatsAppNumber = "+12345678906",
            NationalityId = 1,
            FirstName = "David",
            LastName = "Miller",
            Email = "david.miller@example.com",
            PhoneNumber = "+12345678906",
            Password = "Pass@1234",
            ProfileImageUrl = "https://example.com/images/6.jpg"
        },
        new AddTravelerDto
        {
            Id = 0,
            WhatsAppNumber = "+12345678907",
            NationalityId = 2,
            FirstName = "Eve",
            LastName = "Davis",
            Email = "eve.davis@example.com",
            PhoneNumber = "+12345678907",
            Password = "Pass@1234",
            ProfileImageUrl = "https://example.com/images/7.jpg"
        },
        new AddTravelerDto
        {
            Id = 0,
            WhatsAppNumber = "+12345678908",
            NationalityId = 3,
            FirstName = "Frank",
            LastName = "Wilson",
            Email = "frank.wilson@example.com",
            PhoneNumber = "+12345678908",
            Password = "Pass@1234",
            ProfileImageUrl = "https://example.com/images/8.jpg"
        },
        new AddTravelerDto
        {
            Id = 0,
            WhatsAppNumber = "+12345678909",
            NationalityId = 4,
            FirstName = "Grace",
            LastName = "Moore",
            Email = "grace.moore@example.com",
            PhoneNumber = "+12345678909",
            Password = "Pass@1234",
            ProfileImageUrl = "https://example.com/images/9.jpg"
        },
        new AddTravelerDto
        {
            Id = 0,
            WhatsAppNumber = "+12345678910",
            NationalityId = 5,
            FirstName = "Henry",
            LastName = "Taylor",
            Email = "henry.taylor@example.com",
            PhoneNumber = "+12345678910",
            Password = "Pass@1234",
            ProfileImageUrl = "https://example.com/images/10.jpg"
        },
        new AddTravelerDto
        {
            Id = 0,
            WhatsAppNumber = "+12345678911",
            NationalityId = 1,
            FirstName = "Ivy",
            LastName = "Anderson",
            Email = "ivy.anderson@example.com",
            PhoneNumber = "+12345678911",
            Password = "Pass@1234",
            ProfileImageUrl = "https://example.com/images/11.jpg"
        }
    };
            
          try {
                foreach (var traveler in travelers)
                {
                    var result = await _travelerService.AddAsync(traveler);

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Something got wrong while adding the travelers: {ex.Message}");
            }


        }

    }
}
