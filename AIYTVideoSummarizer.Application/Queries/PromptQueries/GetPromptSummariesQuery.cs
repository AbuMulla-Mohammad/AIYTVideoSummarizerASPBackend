
using AIYTVideoSummarizer.Application.DTOs.PromptDtos;
using MediatR;

namespace AIYTVideoSummarizer.Application.Queries.PromptQueries
{
    public class GetPromptSummariesQuery:IRequest<List<PromptSummariesDto>>
    {
        public Guid Id { get; set; }
    }
}
