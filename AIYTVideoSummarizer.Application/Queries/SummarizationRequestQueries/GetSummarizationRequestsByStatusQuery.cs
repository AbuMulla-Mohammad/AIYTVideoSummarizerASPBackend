
using AIYTVideoSummarizer.Application.DTOs.SummarizationRequestDtos;
using AIYTVideoSummarizer.Domain.Common.Models.PaginationModels;
using AIYTVideoSummarizer.Domain.Enums;
using MediatR;

namespace AIYTVideoSummarizer.Application.Queries.SummarizationRequestQueries
{
    public class GetSummarizationRequestsByStatusQuery:IRequest<PaginatedList<SummarizationRequestDto>>
    {
        public RequestStatus RequestStatus { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
