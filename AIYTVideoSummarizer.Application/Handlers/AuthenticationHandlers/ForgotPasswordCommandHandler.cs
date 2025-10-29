using AIYTVideoSummarizer.Application.Commands.AuthenticationCommands;
using AIYTVideoSummarizer.Application.Common;
using AIYTVideoSummarizer.Application.Interfaces.Common;
using AIYTVideoSummarizer.Application.Models.Email;
using AIYTVideoSummarizer.Domain.Common.Exceptions;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;

namespace AIYTVideoSummarizer.Application.Handlers.AuthenticationHandlers
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailSender _emailSender;
        private readonly AppSettings _appSettings;

        public ForgotPasswordCommandHandler(
            IUserRepository userRepository,
            IEmailSender emailSender,
            IOptions<AppSettings> options)
        {
            _userRepository = userRepository;
            _emailSender = emailSender;
            _appSettings = options.Value;
        }

        public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email)
                ?? throw new NotFoundException(nameof(User));

            var passwordRestToken= Guid.NewGuid().ToString();
            user.PasswordResetToken = passwordRestToken;
            user.PasswordResetTokenExpiry = DateTime.UtcNow.AddMinutes(30);

            await _userRepository.UpdateAsync(user);

            var emailMessage = PrepareForgotPasswordEmail(user, passwordRestToken);
            await _emailSender.SendEmail(emailMessage);

            return Unit.Value;

        }
        private EmailMessage PrepareForgotPasswordEmail(User user, string passwordResetToken)
        {
            var displayName = string.IsNullOrWhiteSpace(user.FirstName)
                ? user.UserName
                : user.FirstName;

            var resetLink = $"{_appSettings.ApiUrl}/api/account/reset-password?token={passwordResetToken}";

            EmailMessage emailMessage = new EmailMessage()
            {
                To = user.Email,
                Subject = "Password Reset Request",
                Body = $@"
                        <html>
                            <body style='font-family: Arial, sans-serif; color: #333;'>
                                <h2>Hello {displayName},</h2>
                                <p>We received a request to reset your password for <b>Samuraizer</b>.</p>
                                <p>Click the button below to set a new password:</p>
        
                                <p style='margin: 24px 0;'>
                                    <a href='{resetLink}' 
                                       style='background-color: #FF5722; color: white; padding: 12px 20px; text-decoration: none; border-radius: 6px;'>
                                       Reset Password
                                    </a>
                                </p>
        
                                <p>If you did not request a password reset, you can safely ignore this email.</p>
                                <br/>
                                <p>Best regards,<br/>The Samuraizer Team</p>
                            </body>
                        </html>"
            };
            return emailMessage;
        }
    }
}
