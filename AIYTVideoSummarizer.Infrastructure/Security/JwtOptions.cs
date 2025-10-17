namespace AIYTVideoSummarizer.Infrastructure.Security
{
    public class JwtOptions
    {
        public string Issuer { get; set; } = string.Empty;
        public string SecretForKey { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;


    }
}
