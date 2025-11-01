
using AIYTVideoSummarizer.Application.DTOs.VideoDtos;
using AIYTVideoSummarizer.Domain.Common.Models.PaginationModels;
using MediatR;

namespace AIYTVideoSummarizer.Application.Queries.VideoQueries
{
    public class GetSummarizedVideosByUserIdQuery:IRequest<PaginatedList<VideoDto>>
    {
        public Guid UserId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchQuery { get; set; }
    }
}
