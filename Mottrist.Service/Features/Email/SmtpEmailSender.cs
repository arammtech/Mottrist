using Microsoft.Extensions.Options;
using Mottrist.Domain.Entities;
using Mottrist.Domain.Global;
using Mottrist.Service.Features.Email.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Mottrist.Service.Features.Email
{
    public class SmtpEmailSender : IEmailSenderStrategy
    {

        private readonly EmailSettings _emailSettings;

        public SmtpEmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task<Result> SendEmailAsync(string toMail, string toName, string subject, string body)
        {
            try
            {
                using (var _mailMessage = new MailMessage())
                {
                    _mailMessage.From = new MailAddress(_emailSettings.SenderEmail,_emailSettings.SenderEmail);
                    _mailMessage.To.Add(new MailAddress(toMail,toName));
                    _mailMessage.Subject = subject;
                    _mailMessage.Body = body;
                    _mailMessage.IsBodyHtml = true;

                    using (var smtpClient = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort))
                    {
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.AppPassword);
                        await smtpClient.SendMailAsync(_mailMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
            return Result.Success();
        }
    }
}
