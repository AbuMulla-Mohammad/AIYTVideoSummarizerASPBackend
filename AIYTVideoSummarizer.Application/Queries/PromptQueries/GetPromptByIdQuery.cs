
using AIYTVideoSummarizer.Application.DTOs.PromptDtos;
using MediatR;

namespace AIYTVideoSummarizer.Application.Queries.PromptQueries
{
    public class GetPromptByIdQuery:IRequest<PromptDto>
    {
        public Guid Id { get; set; }
    }
}
