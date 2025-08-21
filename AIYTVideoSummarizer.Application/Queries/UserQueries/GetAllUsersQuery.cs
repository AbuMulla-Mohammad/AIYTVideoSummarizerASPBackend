
using AIYTVideoSummarizer.Application.DTOs.UserDtos;
using MediatR;

namespace AIYTVideoSummarizer.Application.Queries.UserQueries
{
    public class GetAllUsersQuery:IRequest<List<UserInfoDto>>
    {
    }
}
