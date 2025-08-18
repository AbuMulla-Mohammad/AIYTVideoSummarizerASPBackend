
using AIYTVideoSummarizer.Application.DTOs.SummaryDtos;
using AIYTVideoSummarizer.Application.Queries.UserQueries;
using AIYTVideoSummarizer.Domain.Common.Exceptions;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AIYTVideoSummarizer.Application.Handlers.UserHandlers
{
    public class GetUserSummariesQueryHandler : IRequestHandler<GetUserSummariesQuery, List<UserSummaryDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserSummariesQueryHandler(
            IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserSummaryDto>> Handle(GetUserSummariesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, user => user.Summaries)
                ?? throw new NotFoundException(nameof(User), request.UserId);
            var orderedSummaries = user.Summaries.OrderByDescending(s => s.CreatedAt);
            return _mapper.Map<List<UserSummaryDto>>(orderedSummaries);
        }
    }
}
