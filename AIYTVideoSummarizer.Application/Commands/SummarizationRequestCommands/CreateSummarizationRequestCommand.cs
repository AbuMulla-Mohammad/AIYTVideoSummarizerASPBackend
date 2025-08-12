
using AIYTVideoSummarizer.Application.DTOs.VideoDtos;
using MediatR;

namespace AIYTVideoSummarizer.Application.Commands.SummarizationRequestCommands
{
    public class CreateSummarizationRequestCommand:IRequest<VideoSummaryResponseDto>
    {
        public string YouTubeUrl { get; set; } = string.Empty;
        public Guid PromptId { get; set; }
        public Guid UserId { get; set; }

    }
}
