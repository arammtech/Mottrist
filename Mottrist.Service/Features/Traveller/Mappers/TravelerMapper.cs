using Mottrist.Domain.Entities;
using Mottrist.Domain.Identity;
using Mottrist.Service.Features.Traveller.DTOs;

namespace Mottrist.Service.Features.Traveller.Mappers
{
    /// <summary>
    /// Provides mapping methods between Traveler-related DTOs and entity models.
    /// </summary>
    public static class TravelerMapper
    {
        /// <summary>
        /// Maps properties from <see cref="AddTravelerDto"/> to <see cref="Traveler"/> entity.
        /// </summary>
        public static void Map(AddTravelerDto addTravelerDto, Traveler traveler)
        {
            traveler.WhatsAppNumber = addTravelerDto.WhatsAppNumber;
            traveler.NationalityId = addTravelerDto.NationalityId;
            traveler.ProfileImageUrl = addTravelerDto.ProfileImageUrl;
            traveler.PreferredLanguageId = addTravelerDto.PreferredLanguageId;
            traveler.CityId = addTravelerDto.CityId;
        }

        /// <summary>
        /// Maps properties from <see cref="Traveler"/> entity to <see cref="AddTravelerDto"/>.
        /// </summary>
        public static void Map(Traveler traveler, AddTravelerDto addTravelerDto)
        {
            addTravelerDto.Id = traveler.Id;
            addTravelerDto.WhatsAppNumber = traveler.WhatsAppNumber;
            addTravelerDto.NationalityId = traveler.NationalityId;
            addTravelerDto.ProfileImageUrl = traveler.ProfileImageUrl;
            addTravelerDto.PreferredLanguageId = traveler.PreferredLanguageId;
            addTravelerDto.CityId = traveler.CityId;
        }

        /// <summary>
        /// Maps properties from <see cref="UpdateTravelerDto"/> to <see cref="Traveler"/> entity.
        /// </summary>
        public static void Map(UpdateTravelerDto updateTravelerDto, Traveler traveler)
        {
            traveler.WhatsAppNumber = updateTravelerDto.WhatsAppNumber;
            traveler.NationalityId = updateTravelerDto.NationalityId;
            traveler.ProfileImageUrl = updateTravelerDto.ProfileImageUrl;
            traveler.PreferredLanguageId = updateTravelerDto.PreferredLanguageId;
            traveler.CityId = updateTravelerDto.CityId;
        }

        /// <summary>
        /// Maps properties from <see cref="Traveler"/> entity to <see cref="UpdateTravelerDto"/>.
        /// </summary>
        public static void Map(Traveler traveler, UpdateTravelerDto updateTravelerDto)
        {
            updateTravelerDto.Id = traveler.Id;
            updateTravelerDto.WhatsAppNumber = traveler.WhatsAppNumber;
            updateTravelerDto.NationalityId = traveler.NationalityId;
            updateTravelerDto.ProfileImageUrl = traveler.ProfileImageUrl;
            updateTravelerDto.PreferredLanguageId = traveler.PreferredLanguageId;
            updateTravelerDto.CityId = traveler.CityId;
        }

        /// <summary>
        /// Maps properties from <see cref="Traveler"/> entity to <see cref="ApplicationUser"/>.
        /// </summary>
        public static void Map(Traveler traveler, ApplicationUser applicationUser)
        {
            applicationUser.Id = traveler.UserId;
            applicationUser.FirstName = traveler.User.FirstName;
            applicationUser.LastName = traveler.User.LastName;
            applicationUser.PhoneNumber = traveler.User.PhoneNumber;
            applicationUser.Email = traveler.User.Email;
            applicationUser.UserName = traveler.User.UserName;
        }

        /// <summary>
        /// Maps properties from <see cref="AddTravelerDto"/> to <see cref="ApplicationUser"/>.
        /// </summary>
        public static void Map(AddTravelerDto addTravelerDto, ApplicationUser applicationUser)
        {
            applicationUser.FirstName = addTravelerDto.FirstName;
            applicationUser.LastName = addTravelerDto.LastName;
            applicationUser.Email = addTravelerDto.Email;
            applicationUser.UserName = addTravelerDto.Email;
            applicationUser.PhoneNumber = addTravelerDto.PhoneNumber;
            applicationUser.PasswordHash = addTravelerDto.Password;
        }

        /// <summary>
        /// Maps properties from <see cref="ApplicationUser"/> to <see cref="AddTravelerDto"/>.
        /// </summary>
        public static void Map(ApplicationUser applicationUser, AddTravelerDto addTravelerDto)
        {
            addTravelerDto.FirstName = applicationUser.FirstName;
            addTravelerDto.LastName = applicationUser.LastName;
            addTravelerDto.Email = applicationUser.Email;
            addTravelerDto.PhoneNumber = applicationUser.PhoneNumber;
            addTravelerDto.Password = applicationUser.PasswordHash;
        }

        /// <summary>
        /// Maps properties from <see cref="UpdateTravelerDto"/> to <see cref="ApplicationUser"/>.
        /// </summary>
        public static void Map(UpdateTravelerDto updateTravelerDto, ApplicationUser applicationUser)
        {
            applicationUser.FirstName = updateTravelerDto.FirstName;
            applicationUser.LastName = updateTravelerDto.LastName;
            applicationUser.PhoneNumber = updateTravelerDto.PhoneNumber;
        }

        /// <summary>
        /// Maps properties from <see cref="ApplicationUser"/> to <see cref="UpdateTravelerDto"/>.
        /// </summary>
        public static void Map(ApplicationUser applicationUser, UpdateTravelerDto updateTravelerDto)
        {
            updateTravelerDto.FirstName = applicationUser.FirstName;
            updateTravelerDto.LastName = applicationUser.LastName;
            updateTravelerDto.PhoneNumber = applicationUser.PhoneNumber;
        }
    }
}
