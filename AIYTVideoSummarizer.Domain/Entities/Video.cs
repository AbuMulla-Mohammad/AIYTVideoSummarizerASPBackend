using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIYTVideoSummarizer.Domain.Entities
{
    public class Video
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string YouTubeVideoID { get; set; } = string.Empty;
        public string YouTubeUrl { get; set; } = string.Empty;
        public string? Title { get; set; }
        public string? ChannelName { get; set; }

        public ICollection<SummarizationRequest> SummarizationRequest { get; set; } = new List<SummarizationRequest>();

        public ICollection<Summary> Summaries { get; set; } = new List<Summary>();
        public ICollection<FormattedTranscript> FormattedTranscripts { get; set; } = new List<FormattedTranscript>();

    }
}
