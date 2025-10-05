
using AIYTVideoSummarizer.Application.DTOs.SummaryDtos;
using MediatR;

namespace AIYTVideoSummarizer.Application.Queries.SummaryQueries
{
    public class GetSummariesByPromptIdQuery:IRequest<List<SummaryDto>>
    {
        public Guid PromptId { get; set; }
    }
}
