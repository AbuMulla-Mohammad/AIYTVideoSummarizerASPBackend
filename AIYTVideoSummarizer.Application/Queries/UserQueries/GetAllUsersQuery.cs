
using AIYTVideoSummarizer.Application.DTOs.UserDtos;
using AIYTVideoSummarizer.Domain.Common.Models.PaginationModels;
using MediatR;

namespace AIYTVideoSummarizer.Application.Queries.UserQueries
{
    public class GetAllUsersQuery:IRequest<PaginatedList<UserInfoDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchQuery { get; set; }
    }
}
