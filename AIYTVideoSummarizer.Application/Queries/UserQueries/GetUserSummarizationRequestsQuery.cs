
using AIYTVideoSummarizer.Application.DTOs.SummarizationRequestDtos;
using MediatR;

namespace AIYTVideoSummarizer.Application.Queries.UserQueries
{
    public class GetUserSummarizationRequestsQuery:IRequest<List<UserSummarizationRequestDto>>
    {
        public Guid UserId { get; set; }
    }
}
