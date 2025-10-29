using MediatR;

namespace AIYTVideoSummarizer.Application.Commands.AuthenticationCommands
{
    public class ForgotPasswordCommand : IRequest<Unit>
    {
        public string Email { get; set; } = string.Empty;
    }
}
