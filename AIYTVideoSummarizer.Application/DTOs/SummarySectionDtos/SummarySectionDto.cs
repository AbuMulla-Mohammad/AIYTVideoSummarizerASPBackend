
namespace AIYTVideoSummarizer.Application.DTOs.SummarySectionDtos
{
    public class SummarySectionDto
    {
        public string? Title { get; set; }
        public string Text { get; set; } = string.Empty;
        public double StartTime { get; set; }
        public double EndTime { get; set; }
    }
}
