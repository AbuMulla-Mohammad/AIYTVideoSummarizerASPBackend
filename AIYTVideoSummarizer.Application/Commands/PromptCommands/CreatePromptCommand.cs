
using AIYTVideoSummarizer.Application.DTOs.PromptDtos;
using MediatR;

namespace AIYTVideoSummarizer.Application.Commands.PromptCommands
{
    public class CreatePromptCommand:IRequest<PromptDto>
    {
        public string Name { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
