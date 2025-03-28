using Mottrist.Service.Features.User.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Mottrist.Service.Features.Drivers.DTOs
{
    public class AddUpdateDriverDto : UserDto
    {
        [StringLength(15, ErrorMessage = "WhatsApp number should not exceed 15 characters.")]
        public string? WhatsAppNumber { get; set; }

        [Required(ErrorMessage = "Nationality ID is required.")]
        public int NationailtyId { get; set; }

        [Required(ErrorMessage = "License image URL is required.")]
        [Url(ErrorMessage = "License image URL must be a valid URL.")]
        public string LicenseImageUrl { get; set; } = null!;

        [Range(0, 50, ErrorMessage = "Years of experience must be between 0 and 50.")]
        public byte YearsOfExperience { get; set; }

        [StringLength(250, ErrorMessage = "Bio should not exceed 250 characters.")]
        public string? Bio { get; set; }

        [Required(ErrorMessage = "Passport image URL is required.")]
        [Url(ErrorMessage = "Passport image URL must be a valid URL.")]
        public string PassportImageUrl { get; set; } = null!;

        [Url(ErrorMessage = "Profile image URL must be a valid URL.")]
        public string? ProfileImageUrl { get; set; }

        public bool HasCar { get; set; } = false;

        public int? BrandId { get; set; }

        [Range(1900, 2100, ErrorMessage = "Year must be a valid four-digit year.")]
        public int? Year { get; set; }

        [Range(1, 9, ErrorMessage = "Number of seats must be between 1 and 9.")]
        public byte? NumberOfSeats { get; set; }

        public int? ModelId { get; set; }

        public int? ColorId { get; set; }

        public int? BodyTypeId { get; set; }

        public int? FuelTypeId { get; set; }

        [Url(ErrorMessage = "Car image URL must be a valid URL.")]
        public string? CarImageUrl { get; set; }
    }
}
