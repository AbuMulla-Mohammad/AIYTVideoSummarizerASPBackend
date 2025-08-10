
using AIYTVideoSummarizer.Application.DTOs.SummarySectionDtos;
using AIYTVideoSummarizer.Application.DTOs.TranscriptDtos;

namespace AIYTVideoSummarizer.Application.DTOs.VideoDtos
{
    public class VideoSummaryResponseDto
    {
        public List<SummarySectionDto> SummarySections { get; set; } = new();
        public List<FormattedTranscriptDto> FormattedTranscripts { get; set; } = new();
    }
}
