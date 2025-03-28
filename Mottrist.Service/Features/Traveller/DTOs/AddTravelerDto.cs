namespace Mottrist.Service.Features.Traveller.DTOs
{
    public class AddTravelerDto
    {
        public int Id { get; set; }
        public string? WhatsAppNumber { get; set; }
        public int NationalityId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? PasswordHash { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
