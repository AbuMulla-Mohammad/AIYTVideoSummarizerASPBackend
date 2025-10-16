using MediatR;

namespace AIYTVideoSummarizer.Application.Commands.AuthenticationCommands
{
    public class VerifyEmailCommand:IRequest<Unit>
    {
        public string Token { get; set; } = string.Empty;
    }
}
