

using AIYTVideoSummarizer.Application.DTOs.SummarizationRequestDtos;
using AIYTVideoSummarizer.Domain.Common.Models.PaginationModels;
using MediatR;

namespace AIYTVideoSummarizer.Application.Queries.SummarizationRequestQueries
{
    public class GetAllSummarizationRequestsQuery:IRequest<PaginatedList<SummarizationRequestDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
