using Mottrist.Domain.Common;
using Mottrist.Domain.Entities.CarDetails;
using Mottrist.Domain.Identity;
using Mottrist.Domain.LookupEntities;

namespace Mottrist.Domain.Entities
{
    public class Driver : BaseEntity
    {
        public string? WhatsAppNumber { get; set; }
        public int NationailtyId { get; set; }
        public string LicenseImageUrl { get; set; } = null!;
        public byte YearsOfExperience { get; set; }
        public string? Bio { get; set; }
        public string PassportImageUrl { get; set; } = null!;
        public int? CarId { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;
        public Car? Car { get; set; }
        public Country Country { get; set; } = null!;
    }
}
