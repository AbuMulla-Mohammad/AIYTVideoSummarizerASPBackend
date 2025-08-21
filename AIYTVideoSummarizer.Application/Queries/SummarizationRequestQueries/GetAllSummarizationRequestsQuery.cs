

using AIYTVideoSummarizer.Application.DTOs.SummarizationRequestDtos;
using MediatR;

namespace AIYTVideoSummarizer.Application.Queries.SummarizationRequestQueries
{
    public class GetAllSummarizationRequestsQuery:IRequest<List<SummarizationRequestDto>>
    {
    }
}
