
using AIYTVideoSummarizer.Domain.Entities;
using AIYTVideoSummarizer.Domain.Enums;

namespace AIYTVideoSummarizer.Application.DTOs.SummarizationRequestDtos
{
    public class UserSummarizationRequestDto
    {
        public Guid Id { get; set; } 
        public DateTime RequestedAt { get; set; }
        public string RequestStatus { get; set; } = "Pending";
        public string? ErrorMessage { get; set; }

        public Guid VideoId { get; set; }
        public string? VideoTitle { get; set; }

        public Guid PromptId { get; set; }
        public string PromptUsed { get; set; } = string.Empty;
    }
}
