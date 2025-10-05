
namespace AIYTVideoSummarizer.Application.DTOs.SummaryDtos
{
    public class SummaryDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid VideoId { get; set; }
        public string? VideoTitle { get; set; }

        public Guid PromptId { get; set; }
        public string PromptUsed { get; set; } = string.Empty;
        public int SummarySectionsCount { get; set; }
    }
}
