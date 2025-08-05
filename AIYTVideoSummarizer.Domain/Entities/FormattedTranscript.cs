using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIYTVideoSummarizer.Domain.Entities
{
    public class FormattedTranscript
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }

        public Guid VideoId { get; set; }
        public Video Video { get; set; } = null!;
    }
}
