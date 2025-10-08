using AIYTVideoSummarizer.Application.DTOs.SummaryDtos;
using MediatR;

namespace AIYTVideoSummarizer.Application.Queries.SummaryQueries
{
    public class GetSummariesByUserIdQuery:IRequest<List<SummaryDto>>
    {
        public Guid UserId { get; set; }
    }
}
