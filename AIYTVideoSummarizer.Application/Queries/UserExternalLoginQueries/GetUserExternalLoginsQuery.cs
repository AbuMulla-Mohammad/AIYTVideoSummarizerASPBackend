
using AIYTVideoSummarizer.Application.DTOs.UserExternalLoginDtos;
using MediatR;

namespace AIYTVideoSummarizer.Application.Queries.UserExternalLoginQueries
{
    public class GetUserExternalLoginsQuery:IRequest<List<UserExternalLoginDto>>
    {
        public Guid UserId { get; set; }
    }
}
