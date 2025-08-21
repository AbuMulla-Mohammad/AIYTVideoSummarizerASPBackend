using AIYTVideoSummarizer.Application.DTOs.VideoDtos;
using MediatR;


namespace AIYTVideoSummarizer.Application.Commands.VideoCommands
{
    public class CreateVideoCommand:IRequest<CreateVideoDto>
    {
        public string YouTubeVideoID { get; set; } = string.Empty;
        public string YouTubeUrl { get; set; } = string.Empty;
        public string? Title { get; set; }
        public string? ChannelName { get; set; }
    }
}
