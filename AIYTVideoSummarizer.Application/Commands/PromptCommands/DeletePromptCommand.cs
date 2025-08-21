using AIYTVideoSummarizer.Application.DTOs.PromptDtos;
using MediatR;


namespace AIYTVideoSummarizer.Application.Commands.PromptCommands
{
    public class DeletePromptCommand:IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
