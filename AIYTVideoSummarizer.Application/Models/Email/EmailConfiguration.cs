namespace AIYTVideoSummarizer.Application.Models.Email
{
    public class EmailConfiguration
    {
        public string FromAdress { get; set; } = string.Empty;
        public string FromName { get; set; } = string.Empty;
        public string SmtpServer { get; set; } = string.Empty;
        public int SmtpPort { get; set; }
        public string SmtpUserName { get; set; } = string.Empty;
        public string SmtpPassword { get; set; } = string.Empty;
    }
}
