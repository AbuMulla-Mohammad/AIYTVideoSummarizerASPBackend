
using AIYTVideoSummarizer.Application.DTOs.UserDtos;
using AIYTVideoSummarizer.Application.Queries.UserQueries;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Common.Models.PaginationModels;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;
using MediatR;
using System.Linq.Expressions;

namespace AIYTVideoSummarizer.Application.Handlers.UserHandlers
{
    class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PaginatedList<UserInfoDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(
            IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<UserInfoDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<User, bool>>? filter = null;
            if (!String.IsNullOrWhiteSpace(request.SearchQuery))
            {
                var searchQuery = request.SearchQuery.Trim();
                filter = u => u.UserName.Contains(searchQuery);
            }
            var users = await _userRepository.GetAllAsync(
                request.PageNumber,
                request.PageSize,
                filter);

            return new PaginatedList<UserInfoDto>(
                _mapper.Map<List<UserInfoDto>>(users.Items),
                users.PageData);
        }
    }
}
