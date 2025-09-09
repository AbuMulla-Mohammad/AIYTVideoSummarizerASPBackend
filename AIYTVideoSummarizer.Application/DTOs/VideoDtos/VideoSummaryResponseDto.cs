
using AIYTVideoSummarizer.Application.DTOs.SummarySectionDtos;
using AIYTVideoSummarizer.Application.DTOs.TranscriptDtos;
using System.Text.Json.Serialization;

namespace AIYTVideoSummarizer.Application.DTOs.VideoDtos
{
    public class VideoSummaryResponseDto
    {
        [JsonPropertyName("summary_sections")]
        public List<SummarySectionDto> SummarySections { get; set; } = new();
        [JsonPropertyName("formatted_transcript")]
        public List<FormattedTranscriptDto> FormattedTranscripts { get; set; } = new();
    }
}
