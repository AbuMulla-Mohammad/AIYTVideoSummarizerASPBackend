
using AIYTVideoSummarizer.Application.DTOs.SummarySectionDtos;
using AIYTVideoSummarizer.Domain.Entities;

namespace AIYTVideoSummarizer.Application.DTOs.SummaryDtos
{
    public class SummaryDetailsDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid PromptId { get; set; }
        public string PromptName { get; set; } = string.Empty;

        public string VideoTitle { get; set; } = string.Empty;

        public Guid UserId { get; set; }

        public List<SummarySectionDto> SummarySections { get; set; } = new();
    }
}
