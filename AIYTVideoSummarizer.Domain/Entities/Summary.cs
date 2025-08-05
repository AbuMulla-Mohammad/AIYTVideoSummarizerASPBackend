using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIYTVideoSummarizer.Domain.Entities
{
    public class Summary
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid VideoId { get; set; }
        public Video Video { get; set; } = null!;

        public Guid PromptId { get; set; }
        public Prompt Prompt { get; set; } = null!;

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public ICollection<SummarySection> SummarySections { get; set; } = new List<SummarySection>();

    }
}
