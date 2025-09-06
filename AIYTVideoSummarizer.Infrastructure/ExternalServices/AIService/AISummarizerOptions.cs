
using System.ComponentModel.DataAnnotations;

namespace AIYTVideoSummarizer.Infrastructure.ExternalServices.AIService
{
    public class AISummarizerOptions
    {
        public const string SectionName = "AIService";
        [Required]
        public string BaseUrl { get; set; } = string.Empty;
        [Required]
        public string GetVideoIdEndpoint { get; set; } = "api/get_video_id";
        [Required]
        public string SummarizeEndpoint { get; set; } = "api/summarize_format_transcript";

        [Range(1, 300)]
        public int TimeoutSeconds { get; set; } = 30;

        public int RetryCount { get; set; } = 3;
        public int CircuitBreakerFailures { get; set; } = 5;
        public bool UseSandbox { get; set; } = false;

        public Dictionary<string, string>? DefaultHeaders { get; set; }
    }
}
