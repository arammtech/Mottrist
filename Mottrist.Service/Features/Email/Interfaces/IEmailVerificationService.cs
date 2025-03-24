using Mottrist.Domain.Global;

namespace Mottrist.Service.Features.Email.Interfaces
{
    public interface IEmailVerificationService
    {
        Task<Result> VerifyEmailAsync(int userId, string token);
        string GenerateLinkToVerifyTokenAsync(string token, int userId);
    }
}
