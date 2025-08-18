
using AIYTVideoSummarizer.Application.DTOs.SummarizationRequestDtos;
using AIYTVideoSummarizer.Application.Queries.UserQueries;
using AIYTVideoSummarizer.Domain.Common.Exceptions;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AIYTVideoSummarizer.Application.Handlers.UserHandlers
{
    public class GetUserSummarizationRequestsQueryHandler : IRequestHandler<GetUserSummarizationRequestsQuery, List<UserSummarizationRequestDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserSummarizationRequestsQueryHandler(
            IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<List<UserSummarizationRequestDto>> Handle(GetUserSummarizationRequestsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, user => user.SummarizationRequests)
                ?? throw new NotFoundException(nameof(User), request.UserId);
            var orderdRequests = user.SummarizationRequests.OrderByDescending(sr => sr.RequestedAt);
            return _mapper.Map<List<UserSummarizationRequestDto>>(orderdRequests);
        }
    }
}
