using Mottrist.Domain.Global;

namespace Mottrist.Service.Features.Email.Interfaces
{
    public interface IEmailSenderStrategy
    {
        Task<Result> SendEmailAsync(string toMail,string toName, string subject, string body);
    }
}
