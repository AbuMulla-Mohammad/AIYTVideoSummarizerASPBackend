
using AIYTVideoSummarizer.Application.Commands.AuthenticationCommands;
using AIYTVideoSummarizer.Application.Common;
using AIYTVideoSummarizer.Application.Interfaces.Common;
using AIYTVideoSummarizer.Application.Interfaces.Security;
using AIYTVideoSummarizer.Application.Models.Email;
using AIYTVideoSummarizer.Domain.Common.Exceptions;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;

namespace AIYTVideoSummarizer.Application.Handlers.AuthenticationHandlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserNameGenerator _userNameGenerator;
        private readonly IEmailSender _emailSender;
        private readonly AppSettings _appSettings;

        public RegisterCommandHandler(
            IUserRepository userRepository,
            IMapper mapper,
            IPasswordHasher passwordHasher,
            IUserNameGenerator userNameGenerator,
            IEmailSender emailSender,
            IOptions<AppSettings> options)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _userNameGenerator = userNameGenerator;
            _emailSender = emailSender;
            _appSettings = options.Value;
        }

        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await EnsureUniqueUser(request.Email);

            var user = _mapper.Map<User>(request);
            await PrepareUser(user, request.Password);

            await _userRepository.AddAsync(user);

            try
            {
                var emailMessage = PrepareEmailMessage(user);
                await _emailSender.SendEmail(emailMessage);
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException("Failed to send verification email.", ex);
            }

            return Unit.Value;
        }
        private async Task EnsureUniqueUser(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user is not null)
                throw new ConflictException($"User with email: '{email}' already exists.");
        }

        private async Task PrepareUser(User user, string password)
        {
            var uniqueUserName = await _userNameGenerator.GenerateUniqueUserNameAsync(user.Email);
            if (uniqueUserName is null)
                throw new InvalidOperationException("Can't generate User Name");

            var uniqueUserSalt = _passwordHasher.GenerateSalt();

            var userPasswordHash = _passwordHasher.GenerateHashedPassword(password, uniqueUserSalt);
            if (userPasswordHash is null)
                throw new InvalidOperationException("Can't Hash User Password");

            user.Salt = Convert.ToBase64String(uniqueUserSalt);
            user.UserName = uniqueUserName;
            user.PasswordHash = userPasswordHash;
            user.EmailConfirmationToken = Guid.NewGuid().ToString();

        }

        private EmailMessage PrepareEmailMessage(User user)
        {
            var displayName = string.IsNullOrWhiteSpace(user.FirstName)
                ? user.UserName
                : user.FirstName;

            var verificationLink = $"{_appSettings.ApiUrl}/verify-email?token={user.EmailConfirmationToken}";

            EmailMessage emailMessage = new EmailMessage()
            {
                To = user.Email,
                Subject = "Email verification",
                Body = $@"
                    <html>
                        <body style='font-family: Arial, sans-serif; color: #333;'>
                            <h2>Hello {displayName},</h2>
                            <p>Thank you for signing up for <b>Samuraizer</b>!</p>
                            <p>Please verify your email address by clicking the button below:</p>
                
                            <p style='margin: 24px 0;'>
                                <a href='{verificationLink}' 
                                   style='background-color: #4CAF50; color: white; padding: 12px 20px; text-decoration: none; border-radius: 6px;'>
                                   Verify Email
                                </a>
                            </p>
                
                            <p>If you didn’t create an account, you can safely ignore this email.</p>
                            <br/>
                            <p>Best regards,<br/>The Samuraizer Team</p>
                        </body>
                    </html>"
            };
            return emailMessage;
        }
    }
}
