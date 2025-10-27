using AIYTVideoSummarizer.Application.Commands.AuthenticationCommands;
using AIYTVideoSummarizer.Application.Interfaces.Common;
using AIYTVideoSummarizer.Application.Interfaces.Security;
using AIYTVideoSummarizer.Domain.Common.Exceptions;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using MediatR;
using System.Security.Claims;

namespace AIYTVideoSummarizer.Application.Handlers.AuthenticationHandlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenProvider _tokenProvider;

        public LoginCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            ITokenProvider tokenProvider)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenProvider = tokenProvider;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email.Trim().ToLower())
                ?? throw new InvalidCredentialsException("Invalid email or password.");

            if (!user.IsEmailConfirmed)
                throw new InvalidCredentialsException("Email not verified. Please confirm your email before logging in.");

            if (string.IsNullOrEmpty(user.PasswordHash) || string.IsNullOrEmpty(user.Salt))
                throw new InvalidCredentialsException("Invalid email or password.");
            ValidatePassword(request.Password, user.PasswordHash, user.Salt);

            var claims = GenerateClaims(user);
            return await _tokenProvider.GenerateToken(claims);
        }

        private List<Claim> GenerateClaims(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Role,user.Role.ToString()),

            };
            return claims;
        }

        private void ValidatePassword(string requestPassword, string storedHashedPassowrd,string storedSalt)
        {
            var saltByts = Convert.FromBase64String(storedSalt);
            var isPasswordsEquals=_passwordHasher.VerifyPassword(requestPassword, storedHashedPassowrd, saltByts);

            if (!isPasswordsEquals)
                throw new InvalidCredentialsException("Invalid Email or Password.");
        }
    }
}
