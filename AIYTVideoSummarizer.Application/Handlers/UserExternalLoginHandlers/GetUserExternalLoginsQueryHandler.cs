
using AIYTVideoSummarizer.Application.DTOs.UserExternalLoginDtos;
using AIYTVideoSummarizer.Application.Queries.UserExternalLoginQueries;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace AIYTVideoSummarizer.Application.Handlers.UserExternalLoginHandlers
{
    public class GetUserExternalLoginsQueryHandler : IRequestHandler<GetUserExternalLoginsQuery, List<UserExternalLoginDto>>
    {
        private readonly IUserExternalLoginRepository _userExternalLoginRepository;
        private readonly IMapper _mapper;

        public GetUserExternalLoginsQueryHandler(
            IUserExternalLoginRepository userExternalLoginRepository,
            IMapper mapper)
        {
            _userExternalLoginRepository = userExternalLoginRepository;
            _mapper = mapper;
        }

        public async Task<List<UserExternalLoginDto>> Handle(GetUserExternalLoginsQuery request, CancellationToken cancellationToken)
        {
            var userExternalLogins = await _userExternalLoginRepository.GetByUserIdAsync(request.UserId);
            return _mapper.Map<List<UserExternalLoginDto>>(userExternalLogins);
        }
    }
}
