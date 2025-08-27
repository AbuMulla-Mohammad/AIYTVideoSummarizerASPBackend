using AIYTVideoSummarizer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIYTVideoSummarizer.Domain.Entities
{
    public class SummarizationRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        public RequestStatus RequestStatus { get; set; } = RequestStatus.Pending;
        public string? ErrorMessage { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public Guid? VideoId { get; set; }
        public Video? Video { get; set; } = null!;

        public Guid? PromptId { get; set; }
        public Prompt? Prompt { get; set; } = null!;

    }
}
