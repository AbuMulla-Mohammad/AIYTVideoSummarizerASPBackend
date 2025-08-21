
using AIYTVideoSummarizer.Application.DTOs.SummaryDtos;
using AIYTVideoSummarizer.Application.DTOs.TranscriptDtos;
using AIYTVideoSummarizer.Domain.Entities;

namespace AIYTVideoSummarizer.Application.DTOs.VideoDtos
{
    public class VideoDetailsDto
    {
        public Guid Id { get; set; }
        public string YouTubeVideoID { get; set; } = string.Empty;
        public string YouTubeUrl { get; set; } = string.Empty;
        public string? Title { get; set; }
        public string? ChannelName { get; set; }


        public List<SummaryDetailsDto> Summaries { get; set; } = new();
        public List<FormattedTranscriptDto> FormattedTranscripts { get; set; } = new();

    }
}
