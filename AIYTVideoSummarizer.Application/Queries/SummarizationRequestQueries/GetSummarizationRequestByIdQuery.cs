using AIYTVideoSummarizer.Application.DTOs.SummarizationRequestDtos;
using MediatR;

namespace AIYTVideoSummarizer.Application.Queries.SummarizationRequestQueries
{
    public class GetSummarizationRequestByIdQuery:IRequest<SummarizationRequestDetailsDto>
    {
        public Guid SummarizationRequestId { get; set; }
    }
}
