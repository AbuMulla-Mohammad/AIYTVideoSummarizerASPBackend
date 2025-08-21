
using AIYTVideoSummarizer.Application.DTOs.SummarizationRequestDtos;
using MediatR;

namespace AIYTVideoSummarizer.Application.Queries.SummarizationRequestQueries
{
    public class GetSummarizationRequestsByUserIdQuery:IRequest<List<SummarizationRequestDto>>
    {
        public Guid UserId { get; set; }
    }
}
