namespace Mottrist.Service.Features.Traveller.DTOs
{
    public class UpdateTravelerDto
    {
        public int Id { get; set; }
        public string? WhatsAppNumber { get; set; }
        public int NationalityId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
