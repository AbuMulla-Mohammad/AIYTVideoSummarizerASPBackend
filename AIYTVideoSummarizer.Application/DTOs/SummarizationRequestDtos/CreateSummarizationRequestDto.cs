using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIYTVideoSummarizer.Application.DTOs.SummarizationRequestDtos
{
    public class CreateSummarizationRequestDto
    {
        public string YouTubeUrl { get; set; } = string.Empty;
        public Guid PromptId { get; set; }
    }
}
