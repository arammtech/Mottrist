using Mottrist.Domain.Global;

namespace Mottrist.Service.Features.Email.Interfaces
{
    public interface IEmailService
    {
        Task<Result> SendEmailAsync(string toMail, string toName, string subject, string body);
        void SwitchEmailSenderStrategy(IEmailSenderStrategy emailSenderStrategy);
    }
}
