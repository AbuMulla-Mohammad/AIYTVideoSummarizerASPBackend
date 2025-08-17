
using AIYTVideoSummarizer.Application.DTOs.UserDtos;
using MediatR;

namespace AIYTVideoSummarizer.Application.Queries.UserQueries
{
    public class GetUserProfileQuery:IRequest<UserProfileDto>
    {
        public Guid UserId { get; set; }
    }
}
