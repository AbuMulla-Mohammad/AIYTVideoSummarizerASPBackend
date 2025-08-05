using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIYTVideoSummarizer.Domain.Entities
{
    public class SummarySection
    {
        public int Id { get; set; } 
        public string? Title { get; set; }
        public string Text { get; set; } = string.Empty;
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }

        public Guid SummaryId { get; set; }
        public Summary Summary { get; set; } = null!;
    }
}
