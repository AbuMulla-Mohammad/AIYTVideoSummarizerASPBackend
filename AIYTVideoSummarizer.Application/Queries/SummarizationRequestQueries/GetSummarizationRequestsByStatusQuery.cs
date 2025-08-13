
using AIYTVideoSummarizer.Application.DTOs.SummarizationRequestDtos;
using AIYTVideoSummarizer.Domain.Enums;
using MediatR;

namespace AIYTVideoSummarizer.Application.Queries.SummarizationRequestQueries
{
    public class GetSummarizationRequestsByStatusQuery:IRequest<List<SummarizationRequestDto>>
    {
        public RequestStatus RequestStatus { get; set; }
    }
}
