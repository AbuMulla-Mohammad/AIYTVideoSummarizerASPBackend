
using AIYTVideoSummarizer.Domain.Entities;

namespace AIYTVideoSummarizer.Application.DTOs.PromptDtos
{
    public class PromptDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
