using AIYTVideoSummarizer.Application.DTOs.UserDtos;
using MediatR;

namespace AIYTVideoSummarizer.Application.Queries.UserQueries
{
    public class GetUserByIdQuery:IRequest<UserInfoDto>
    {
        public Guid UserId { get; set; }
    }
}
