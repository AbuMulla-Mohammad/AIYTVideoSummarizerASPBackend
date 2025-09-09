
using System.Text.Json.Serialization;

namespace AIYTVideoSummarizer.Application.DTOs.TranscriptDtos
{
    public class FormattedTranscriptDto
    {
        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;
        [JsonPropertyName("start")]
        public double StartTime { get; set; }
        [JsonPropertyName("end")]
        public double EndTime { get; set; }
    }
}
