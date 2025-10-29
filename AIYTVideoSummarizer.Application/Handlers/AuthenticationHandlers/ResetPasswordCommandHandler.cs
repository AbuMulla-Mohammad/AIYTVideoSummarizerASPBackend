using AIYTVideoSummarizer.Application.Commands.AuthenticationCommands;
using AIYTVideoSummarizer.Application.Interfaces.Security;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using MediatR;

namespace AIYTVideoSummarizer.Application.Handlers.AuthenticationHandlers
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public ResetPasswordCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByPasswordResetTokenAsync(request.Token)
                ?? throw new UnauthorizedAccessException("Invalid or expired password reset token.");

            user.PasswordResetToken = null;
            user.PasswordResetTokenExpiry = null;

            var newUserSalt = _passwordHasher.GenerateSalt();
            user.Salt = Convert.ToBase64String(newUserSalt);

            var newUserPassword = _passwordHasher.GenerateHashedPassword(request.NewPassword, newUserSalt);
            user.PasswordHash = newUserPassword;

            await _userRepository.UpdateAsync(user);

            return Unit.Value;
        }
    }
}
