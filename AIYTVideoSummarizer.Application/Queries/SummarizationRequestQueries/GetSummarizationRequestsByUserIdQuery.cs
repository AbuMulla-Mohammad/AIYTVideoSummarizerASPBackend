
using AIYTVideoSummarizer.Application.DTOs.SummarizationRequestDtos;
using AIYTVideoSummarizer.Domain.Common.Models.PaginationModels;
using MediatR;

namespace AIYTVideoSummarizer.Application.Queries.SummarizationRequestQueries
{
    public class GetSummarizationRequestsByUserIdQuery:IRequest<PaginatedList<SummarizationRequestDto>>
    {
        public Guid UserId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
