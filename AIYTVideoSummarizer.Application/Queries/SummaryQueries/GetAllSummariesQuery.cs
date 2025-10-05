
using AIYTVideoSummarizer.Application.DTOs.SummaryDtos;
using MediatR;

namespace AIYTVideoSummarizer.Application.Queries.SummaryQueries
{
    public class GetAllSummariesQuery:IRequest<List<SummaryDto>>
    {
    }
}
