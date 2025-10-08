
using AIYTVideoSummarizer.Application.DTOs.SummaryDtos;
using MediatR;

namespace AIYTVideoSummarizer.Application.Queries.SummaryQueries
{
    public class GetSummaryByIdQuery:IRequest<SummaryDetailsDto>
    {
        public Guid SummaryId { get; set; }
    }
}
