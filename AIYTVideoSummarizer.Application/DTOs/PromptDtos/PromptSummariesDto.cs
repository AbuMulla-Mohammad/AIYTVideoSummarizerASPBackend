
using AIYTVideoSummarizer.Application.DTOs.SummaryDtos;

namespace AIYTVideoSummarizer.Application.DTOs.PromptDtos
{
    public class PromptSummariesDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<SummaryDetailsDto> Summaries { get; set; } = new();

    }
}
