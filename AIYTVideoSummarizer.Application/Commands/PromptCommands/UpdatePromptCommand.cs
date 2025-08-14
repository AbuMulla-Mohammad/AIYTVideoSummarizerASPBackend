using AIYTVideoSummarizer.Application.DTOs.PromptDtos;
using MediatR;

namespace AIYTVideoSummarizer.Application.Commands.PromptCommands
{
    public class UpdatePromptCommand:IRequest<PromptDto?>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } 
        public string? Text { get; set; } 
        public string? Description { get; set; }
    }
}
