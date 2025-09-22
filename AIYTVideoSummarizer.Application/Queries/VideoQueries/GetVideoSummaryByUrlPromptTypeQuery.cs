using AIYTVideoSummarizer.Application.DTOs.VideoDtos;
using MediatR;

namespace AIYTVideoSummarizer.Application.Queries.VideoQueries
{
    public class GetVideoSummaryByUrlPromptTypeQuery:IRequest<VideoSummaryResponseDto>
    {
        public string VideoUrl { get; set; } = string.Empty;
        public string PromptName { get; set; }= string.Empty;
    }
}
