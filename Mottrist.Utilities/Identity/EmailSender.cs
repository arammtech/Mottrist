using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;

namespace Mottrist.Utilities.Identity
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender( IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            using (var mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(_configuration["EmailSettings:fromEmail"]);
                mailMessage.To.Add(email);
                mailMessage.Subject = subject;
                mailMessage.Body = htmlMessage;
                mailMessage.IsBodyHtml = true;

                using (var smtpClient = new SmtpClient(_configuration["EmailSettings:smtpHost"], Convert.ToInt32(_configuration["EmailSettings:smtpPort"])))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new NetworkCredential(_configuration["EmailSettings:smtpUser"], _configuration["EmailSettings:smtpPass"] );
                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
        }
    }
}
