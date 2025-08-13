
using AIYTVideoSummarizer.Application.DTOs.VideoDtos;
using MediatR;

namespace AIYTVideoSummarizer.Application.Queries.VideoQueries
{
    public class GetSummarizedVideosByUserIdQuery:IRequest<List<VideoDto>?>
    {
        public Guid UserId { get; set; }
    }
}
