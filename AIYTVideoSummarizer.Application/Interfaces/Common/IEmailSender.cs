using AIYTVideoSummarizer.Application.Models.Email;

namespace AIYTVideoSummarizer.Application.Interfaces.Common
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(EmailMessage email);
    }
}
