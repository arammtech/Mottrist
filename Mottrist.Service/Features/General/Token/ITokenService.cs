using Mottrist.Domain.Identity;

namespace Mottrist.Service.Features.General.Token
{
    public interface ITokenService
    {
        Task<string> GenerateToken(ApplicationUser user);
        string ShortenToken(string token);
        string DecodeShortenToken(string shortToken);
    }
}
