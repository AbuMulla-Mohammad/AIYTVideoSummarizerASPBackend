
using AIYTVideoSummarizer.Application.DTOs.SummaryDtos;
using MediatR;

namespace AIYTVideoSummarizer.Application.Queries.UserQueries
{
    public class GetUserSummariesQuery:IRequest<List<UserSummaryDto>>
    {
        public Guid UserId { get; set; }
    }
}
