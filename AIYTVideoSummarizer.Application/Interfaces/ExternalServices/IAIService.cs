
using AIYTVideoSummarizer.Application.DTOs.VideoDtos;

namespace AIYTVideoSummarizer.Application.Interfaces.ExternalServices
{
    public interface IAIService
    {
        Task<string> GetYTVideoId(string videoUrl);
        Task<VideoSummaryResponseDto> SummarizeVideo(string videoUrl,Guid promptId);
    }
}
