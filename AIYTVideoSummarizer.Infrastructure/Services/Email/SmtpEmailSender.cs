using AIYTVideoSummarizer.Application.Interfaces.Common;
using AIYTVideoSummarizer.Application.Models.Email;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace AIYTVideoSummarizer.Infrastructure.Services.Email
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfiguration;

        public SmtpEmailSender(IOptions<EmailConfiguration> emailConfiguration)
        {
            _emailConfiguration = emailConfiguration.Value;
        }

        public async Task<bool> SendEmail(EmailMessage email)
        {
            using(var clinet=new SmtpClient())
            {
                var emailMessage = new MailMessage()
                {
                    From = new MailAddress(_emailConfiguration.FromAdress, _emailConfiguration.FromName),
                    Subject = email.Subject,
                    Body = email.Body,
                    IsBodyHtml = true
                };
                emailMessage.To.Add(email.To);

                clinet.Host = _emailConfiguration.SmtpServer;
                clinet.Port = _emailConfiguration.SmtpPort;
                clinet.UseDefaultCredentials = false;
                clinet.Credentials = new NetworkCredential(_emailConfiguration.SmtpUserName,
                    _emailConfiguration.SmtpPassword);
                clinet.EnableSsl = true;

                await clinet.SendMailAsync(emailMessage);
                return true;
            }
        }
    }
}
