using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIYTVideoSummarizer.Domain.Entities
{
    public class Prompt
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<SummarizationRequest> SummarizationRequests { get; set; } = new List<SummarizationRequest>();
        public List<Summary> Summaries { get; set; } = new();
    }
}
