using MediatR;

namespace AIYTVideoSummarizer.Application.Commands.AuthenticationCommands
{
    public class ExternalGoogleLoginCommand:IRequest<string>
    {
        public string IdToken { get; set; } = string.Empty;
    }
}
