using AIYTVideoSummarizer.Application.DTOs.VideoDtos;
using MediatR;

namespace AIYTVideoSummarizer.Application.Queries.VideoQueries
{
    public class GetVideoSummaryByUrlPromptTypeQuery:IRequest<VideoSummaryResponseDto>
    {
        public string VideoUrl { get; }
        public string PromptName { get; }
        public GetVideoSummaryByUrlPromptTypeQuery(string videoUrl,string promptName)
        {
            VideoUrl = videoUrl;
            PromptName = promptName;
        }
    }
}
