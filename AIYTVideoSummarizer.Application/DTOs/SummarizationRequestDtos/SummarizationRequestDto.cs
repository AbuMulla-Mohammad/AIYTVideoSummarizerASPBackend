
using AIYTVideoSummarizer.Domain.Entities;
using AIYTVideoSummarizer.Domain.Enums;

namespace AIYTVideoSummarizer.Application.DTOs.SummarizationRequestDtos
{
    public class SummarizationRequestDto
    {
        public Guid Id { get; set; }
        public DateTime RequestedAt { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public string? ErrorMessage { get; set; }

        public Guid UserId { get; set; }

        public Guid VideoId { get; set; }

        public Guid PromptId { get; set; }
    }
}
