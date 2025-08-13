using AIYTVideoSummarizer.Domain.Enums;

namespace AIYTVideoSummarizer.Application.DTOs.SummarizationRequestDtos
{
    public class SummarizationRequestDetailsDto
    {
        public Guid Id { get; set; }
        public DateTime RequestedAt { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public string? ErrorMessage { get; set; }

        public Guid UserId { get; set; }

        public Guid VideoId { get; set; }
        public string YouTubeUrl { get; set; } = string.Empty;

        public Guid PromptId { get; set; }
        public string PromptName { get; set; } = string.Empty;
        public string PromptDescription { get; set; } = string.Empty;
    }
}
