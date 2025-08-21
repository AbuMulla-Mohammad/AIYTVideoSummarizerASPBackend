
using AIYTVideoSummarizer.Application.DTOs.VideoDtos;
using MediatR;

namespace AIYTVideoSummarizer.Application.Commands.VideoCommands
{
    public class DeleteVideoCommand:IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
