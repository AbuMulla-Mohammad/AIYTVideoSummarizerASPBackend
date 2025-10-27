using AIYTVideoSummarizer.Application.Commands.AuthenticationCommands;
using AIYTVideoSummarizer.Application.Common;
using AIYTVideoSummarizer.Application.Interfaces.Common;
using AIYTVideoSummarizer.Application.Interfaces.Security;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using Google.Apis.Auth;
using MediatR;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace AIYTVideoSummarizer.Application.Handlers.AuthenticationHandlers
{
    public class ExternalGoogleLoginCommandHandler : IRequestHandler<ExternalGoogleLoginCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserExternalLoginRepository _userExternalLoginRepository;
        private readonly ITokenProvider _tokenProvider;
        private readonly IUserNameGenerator _userNameGenerator;
        private readonly GoogleSettings _googleSettings;

        public ExternalGoogleLoginCommandHandler(
            IUserRepository userRepository,
            IUserExternalLoginRepository userExternalLoginRepository,
            ITokenProvider tokenProvider,
            IUserNameGenerator userNameGenerator,
            IOptions<GoogleSettings> options)
        {
            _userRepository = userRepository;
            _userExternalLoginRepository = userExternalLoginRepository;
            _tokenProvider = tokenProvider;
            _userNameGenerator = userNameGenerator;
            _googleSettings = options.Value;
        }

        public async Task<string> Handle(ExternalGoogleLoginCommand request, CancellationToken cancellationToken)
        {
            GoogleJsonWebSignature.Payload payload;
            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _googleSettings.ClientId }
                });
            }
            catch (InvalidJwtException ex)
            {
                throw new UnauthorizedAccessException("Invalid Google token", ex);
            }

            var existingLogin = await _userExternalLoginRepository.GetByProviderAsync("Google", payload.Subject);

            if(existingLogin is not null)
            {
                var existingUser = existingLogin.User;
                var existingUserClaims = GenerateClaims(existingUser);
                return await _tokenProvider.GenerateToken(existingUserClaims);
            }

            var user = await _userRepository.GetByEmailAsync(payload.Email);

            if(user is null)
            {
                user = new User();
                await PrepareUser(user, payload);
                await _userRepository.AddAsync(user);
            }

            await _userExternalLoginRepository.AddAsync(new UserExternalLogin
            {
                UserId = user.Id,
                LoginProvider = "Google",
                ProviderKey = payload.Subject
            });

            var userClaims = GenerateClaims(user);
            return await _tokenProvider.GenerateToken(userClaims);

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
        private async Task PrepareUser(User user,GoogleJsonWebSignature.Payload payload)
        {
            var uniqueUserName = await _userNameGenerator.GenerateUniqueUserNameAsync(payload.Email);
            if (uniqueUserName is null)
                throw new InvalidOperationException("Can't generate User Name");
            user.FirstName = payload.GivenName;
            user.LastName = payload.FamilyName;
            user.ProfilePictureUrl = payload.Picture;
            user.Email = payload.Email;
            user.UserName = uniqueUserName;
            user.IsEmailConfirmed = true;
        }
      
    }
}
