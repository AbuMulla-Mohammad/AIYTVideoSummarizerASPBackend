
namespace AIYTVideoSummarizer.Application.DTOs.TranscriptDtos
{
    public class FormattedTranscriptDto
    {
        public string Text { get; set; } = string.Empty;
        public double StartTime { get; set; }
        public double EndTime { get; set; }
    }
}
