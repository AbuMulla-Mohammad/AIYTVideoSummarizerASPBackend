
using AIYTVideoSummarizer.Application.DTOs.SummaryDtos;
using AIYTVideoSummarizer.Domain.Common.Models.PaginationModels;
using MediatR;

namespace AIYTVideoSummarizer.Application.Queries.SummaryQueries
{
    public class GetAllSummariesQuery:IRequest<PaginatedList<SummaryDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchQuery { get; set; }
    }
}
