
using AIYTVideoSummarizer.Application.DTOs.VideoDtos;
using MediatR;

namespace AIYTVideoSummarizer.Application.Commands.VideoCommands
{
    public class DeleteVideoCommand:IRequest<VideoDto>
    {
        public Guid Id { get; set; }
    }
}
