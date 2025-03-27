using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottrist.Service.Features.Drivers.DTOs
{
    public class DriverDto
    {
        public int Id { get; set; }
        public string? WhatsAppNumber { get; set; }
        public string Nationailty { get; set; }
        public string LicenseImageUrl { get; set; } = null!;
        public byte YearsOfExperience { get; set; }
        public string? Bio { get; set; }
        public string PassportImageUrl { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PasswordHash { get; set; }
        public string? ProfileImageUrl { get; set; }

        public bool HasCar { get; set; } = false;
        public string? CarBrand { get; set; }
        public int? CarYear { get; set; }
        public byte? CarNumberOfSeats { get; set; }
        public string? CarModel { get; set; }
        public string? CarColor { get; set; }
        public string? CarBodyType { get; set; }
        public string? CarFuelType { get; set; }
        public string? CarImageUrl { get; set; }
    }
}
