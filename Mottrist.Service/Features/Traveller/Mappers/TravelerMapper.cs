using Mottrist.Domain.Entities;
using Mottrist.Domain.Identity;
using Mottrist.Service.Features.Traveller.DTOs;

namespace Mottrist.Service.Features.Traveller.Mappers
{
    public static class TravelerMapper
    {
        // Map properties from AddTravelerDto to Traveler entity.
        public static void Map(AddTravelerDto addTravelerDto, Traveler traveler)
        {
            traveler.WhatsAppNumber = addTravelerDto.WhatsAppNumber;
            traveler.NationalityId = addTravelerDto.NationalityId;
            traveler.ProfileImageUrl = addTravelerDto.ProfileImageUrl;
        }

        // Map properties from Traveler entity to AddTravelerDto.
        public static void Map(Traveler traveler, AddTravelerDto addTravelerDto)
        {
            addTravelerDto.Id = traveler.Id;
            addTravelerDto.WhatsAppNumber = traveler.WhatsAppNumber;
            addTravelerDto.NationalityId = traveler.NationalityId;
            addTravelerDto.ProfileImageUrl = traveler.ProfileImageUrl;
        }

        // Map properties from updateTravelerDto entity to Traveler.
        public static void Map(UpdateTravelerDto updateTravelerDto, Traveler traveler)
        {
            traveler.WhatsAppNumber = updateTravelerDto.WhatsAppNumber;
            traveler.NationalityId = updateTravelerDto.NationalityId;
            traveler.ProfileImageUrl = updateTravelerDto.ProfileImageUrl;
        }

        // Map properties from Traveler entity to updateTravelerDto.
        public static void Map(Traveler traveler, UpdateTravelerDto updateTravelerDto)
        {
            updateTravelerDto.Id = traveler.Id;
            updateTravelerDto.WhatsAppNumber = traveler.WhatsAppNumber;
            updateTravelerDto.NationalityId = traveler.NationalityId;
            updateTravelerDto.ProfileImageUrl = traveler.ProfileImageUrl;
        }

        // Map properties from Traveler entity to ApplicationUser.
        public static void Map(Traveler traveler, ApplicationUser applicationUser)
        {
            applicationUser.Id = traveler.UserId;
            applicationUser.FirstName = traveler.User.FirstName;
            applicationUser.LastName = traveler.User.LastName;
            applicationUser.PhoneNumber = traveler.User.PhoneNumber;
            applicationUser.Email = traveler.User.Email;
            applicationUser.UserName = traveler.User.UserName;
        }

        // Map properties from AddTravelerDto to ApplicationUser.
        public static void Map(AddTravelerDto addTravelerDto, ApplicationUser applicationUser)
        {
            // Assuming ApplicationUser has these properties.
            applicationUser.FirstName = addTravelerDto.FirstName;
            applicationUser.LastName = addTravelerDto.LastName;
            applicationUser.Email = addTravelerDto.Email;
            applicationUser.UserName = addTravelerDto.Email;
            applicationUser.PhoneNumber = addTravelerDto.PhoneNumber;
            applicationUser.PasswordHash = addTravelerDto.Password;

        }

        // Map properties from ApplicationUser to AddTravelerDto.
        public static void Map(ApplicationUser applicationUser, AddTravelerDto addTravelerDto)
        {
            addTravelerDto.FirstName = applicationUser.FirstName;
            addTravelerDto.LastName = applicationUser.LastName;
            addTravelerDto.Email = applicationUser.Email;
            addTravelerDto.PhoneNumber = applicationUser.PhoneNumber;
            addTravelerDto.Password = applicationUser.PasswordHash;
        }

        // Map properties from UpdateTravelerDto to ApplicationUser.
        public static void Map(UpdateTravelerDto updateTravelerDto, ApplicationUser applicationUser)
        {
            applicationUser.FirstName = updateTravelerDto.FirstName;
            applicationUser.LastName = updateTravelerDto.LastName;
            applicationUser.PhoneNumber = updateTravelerDto.PhoneNumber;
        }

        // Map properties from ApplicationUser to UpdateTravelerDto.
        public static void Map(ApplicationUser applicationUser, UpdateTravelerDto updateTravelerDto)
        {
            updateTravelerDto.FirstName = applicationUser.FirstName;
            updateTravelerDto.LastName = applicationUser.LastName;
            updateTravelerDto.PhoneNumber = applicationUser.PhoneNumber;
        }
    }
}
