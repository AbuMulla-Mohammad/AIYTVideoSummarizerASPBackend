
using System.Text.Json.Serialization;

namespace AIYTVideoSummarizer.Application.DTOs.SummarySectionDtos
{
    public class SummarySectionDto
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }
        [JsonPropertyName("summary")]
        public string Text { get; set; } = string.Empty;
        [JsonPropertyName("start")]
        public double StartTime { get; set; }
        [JsonPropertyName("end")]
        public double EndTime { get; set; }
    }
}
