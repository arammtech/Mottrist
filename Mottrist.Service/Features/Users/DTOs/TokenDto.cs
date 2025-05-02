namespace Mottrist.Service.Features.Users.DTOs
{
    public class TokenDto
    {
        public string token { get; set; }
        public DateTime expiredDateTime { get; set; }
    }
}