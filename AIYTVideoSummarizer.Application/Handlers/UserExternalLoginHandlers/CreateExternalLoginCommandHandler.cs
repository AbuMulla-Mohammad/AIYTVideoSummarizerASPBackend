
using AIYTVideoSummarizer.Application.Commands.UserExternalLoginCommands;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AIYTVideoSummarizer.Application.Handlers.UserExternalLoginHandlers
{
    public class CreateExternalLoginCommandHandler : IRequestHandler<CreateExternalLoginCommand, Guid>
    {
        private readonly IUserExternalLoginRepository _userExternalLoginRepository;
        private readonly IMapper _mapper;

        public CreateExternalLoginCommandHandler(
            IUserExternalLoginRepository userExternalLoginRepository,
            IMapper mapper)
        {
            _userExternalLoginRepository = userExternalLoginRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateExternalLoginCommand request, CancellationToken cancellationToken)
        {
            var userExternalLogin = await _userExternalLoginRepository.AddAsync(_mapper.Map<UserExternalLogin>(request));
            return userExternalLogin.Id;
        }
    }
}
