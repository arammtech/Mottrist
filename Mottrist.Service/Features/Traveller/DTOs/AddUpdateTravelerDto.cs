namespace Mottrist.Service.Features.Traveller.DTOs
{
    public class AddUpdateTravelerDto
    {
        public int Id { get; set; }
        public string? WhatsAppNumber { get; set; }
        public int NationailtyId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PasswordHash { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
