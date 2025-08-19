
using AIYTVideoSummarizer.Application.Commands.UserExternalLoginCommands;
using AIYTVideoSummarizer.Domain.Common.Exceptions;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AIYTVideoSummarizer.Application.Handlers.UserExternalLoginHandlers
{
    public class DeleteExternalLoginCommandHandler : IRequestHandler<DeleteExternalLoginCommand, Unit>
    {
        private readonly IUserExternalLoginRepository _userExternalLoginRepository;

        public DeleteExternalLoginCommandHandler(IUserExternalLoginRepository userExternalLoginRepository)
        {
            _userExternalLoginRepository = userExternalLoginRepository;
        }

        public async Task<Unit> Handle(DeleteExternalLoginCommand request, CancellationToken cancellationToken)
        {
            var userExternalLogin = await _userExternalLoginRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(UserExternalLogin), request.Id);
            await _userExternalLoginRepository.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
