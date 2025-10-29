using AIYTVideoSummarizer.Application.Commands.AuthenticationCommands;
using AIYTVideoSummarizer.Domain.Common.Exceptions;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AIYTVideoSummarizer.Application.Handlers.AuthenticationHandlers
{
    public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public VerifyEmailCommandHandler(
            IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailConfirmationTokenAsync(request.Token)
                ?? throw new NotFoundException(nameof(User));

            if (!user.IsEmailConfirmed)
            {
                user.IsEmailConfirmed = true;
                user.EmailConfirmationToken = null;
                await _userRepository.UpdateAsync(user);
            }
            return Unit.Value;
        }
    }
}
