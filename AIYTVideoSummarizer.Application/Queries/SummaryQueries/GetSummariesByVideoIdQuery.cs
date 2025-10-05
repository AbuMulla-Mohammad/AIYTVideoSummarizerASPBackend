
using AIYTVideoSummarizer.Application.DTOs.SummaryDtos;
using MediatR;

namespace AIYTVideoSummarizer.Application.Queries.SummaryQueries
{
    public class GetSummariesByVideoIdQuery:IRequest<List<SummaryDto>>
    {
        public Guid VideoId { get; set; }
    }
}
