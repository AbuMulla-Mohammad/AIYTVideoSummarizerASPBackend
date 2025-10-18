using AIYTVideoSummarizer.Application.Commands.AuthenticationCommands;
using AIYTVideoSummarizer.Application.Interfaces.Security;
using AIYTVideoSummarizer.Domain.Common.Exceptions;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using MediatR;

namespace AIYTVideoSummarizer.Application.Handlers.AuthenticationHandlers
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public ChangePasswordCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine(request.UserId);
            var user = await _userRepository.GetByIdAsync(request.UserId)
                ?? throw new NotFoundException(nameof(User));

            if (string.IsNullOrEmpty(user.PasswordHash) || string.IsNullOrEmpty(user.Salt))
                throw new InvalidOperationException("You cannot change the password because you registered via an external provider.");

            VerifyPassword(request.OldPassword, user.PasswordHash, user.Salt);

            var newSalt = _passwordHasher.GenerateSalt();
            user.Salt = Convert.ToBase64String(newSalt);

            var newPasswordHashed = _passwordHasher.GenerateHashedPassword(request.NewPassword, newSalt);
            user.PasswordHash = newPasswordHashed;

            await _userRepository.UpdateAsync(user);

            return Unit.Value;

        }

        private void VerifyPassword(string requestPassword, string storedHashedPassword, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            var isPasswordsEquals = _passwordHasher.VerifyPassword(requestPassword, storedHashedPassword, saltBytes);

            if(!isPasswordsEquals)
                throw new InvalidCredentialsException("Invalid Password.");
        }


    }
}
