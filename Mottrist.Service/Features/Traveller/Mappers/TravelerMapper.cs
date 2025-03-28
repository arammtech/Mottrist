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
            // traveler.Id = addTravelerDto.Id; 
            traveler.WhatsAppNumber = addTravelerDto.WhatsAppNumber;
            traveler.NationalityId = addTravelerDto.NationalityId;
            traveler.ProfileImageUrl = addTravelerDto.ProfileImageUrl;
            //traveler.User.FirstName = addTravelerDto.FirstName;
            //traveler.User.LastName = addTravelerDto.LastName;
            //traveler.User.Email = addTravelerDto.Email;
            //traveler.User.PhoneNumber = addTravelerDto.PhoneNumber;
            //traveler.User.PasswordHash = addTravelerDto.PasswordHash;
        }

        // Map properties from Traveler entity to AddTravelerDto.
        public static void Map(Traveler traveler, AddTravelerDto addTravelerDto)
        {
            addTravelerDto.Id = traveler.Id;
            addTravelerDto.WhatsAppNumber = traveler.WhatsAppNumber;
            addTravelerDto.NationalityId = traveler.NationalityId;
            addTravelerDto.ProfileImageUrl = traveler.ProfileImageUrl;
            // Note: Other properties like FirstName, LastName, Email, PhoneNumber, and PasswordHash
            // come from the associated ApplicationUser and should be mapped separately.
        }

        // Map properties from AddTravelerDto to ApplicationUser.
        public static void Map(AddTravelerDto addTravelerDto, ApplicationUser applicationUser)
        {
            // Assuming ApplicationUser has these properties.
            applicationUser.FirstName = addTravelerDto.FirstName;
            applicationUser.LastName = addTravelerDto.LastName;
            applicationUser.Email = addTravelerDto.Email;
            applicationUser.PhoneNumber = addTravelerDto.PhoneNumber;
            applicationUser.PasswordHash = addTravelerDto.PasswordHash;
        }

        // Map properties from ApplicationUser to AddTravelerDto.
        public static void Map(ApplicationUser applicationUser, AddTravelerDto addTravelerDto)
        {
            addTravelerDto.FirstName = applicationUser.FirstName;
            addTravelerDto.LastName = applicationUser.LastName;
            addTravelerDto.Email = applicationUser.Email;
            addTravelerDto.PhoneNumber = applicationUser.PhoneNumber;
            addTravelerDto.PasswordHash = applicationUser.PasswordHash;
        }
    }
}
