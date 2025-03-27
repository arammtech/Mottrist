using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottrist.Service.Features.Drivers.DTOs
{
    public class AddUpdateDriverDto
    {
        public int Id { get; set; }
        public string? WhatsAppNumber { get; set; }
        public int NationailtyId { get; set; }
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
    //    public int? CarId { get; }
        public int? BrandId { get; set; }
        public int? Year { get; set; }
        public byte? NumberOfSeats { get; set; }
        public int? ModelId { get; set; }
        public int? ColorId { get; set; }
        public int? BodyTypeId { get; set; }
        public int? FuelTypeId { get; set; }
        public string? CarImageUrl { get; set; }
    }
}
