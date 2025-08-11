
using AIYTVideoSummarizer.Application.DTOs.VideoDtos;
using MediatR;

namespace AIYTVideoSummarizer.Application.Queries.VideoQueries
{
    public class GetVideoByIdQuery:IRequest<VideoDetailsDto?>
    {
        public Guid VideoId { get; set; }
        public GetVideoByIdQuery(Guid videoId)
        {
            VideoId = videoId;
        }
    }
}
